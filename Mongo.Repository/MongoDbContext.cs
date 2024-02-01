using System.Linq.Expressions;
using MongoDB.Driver;

namespace Mongo.Repository
{
    public class MongoDbContext : IMongoDbContext
    {
        private string databaseName;
        private MongoConfiguration mongoConfig = new MongoConfiguration().Executa();

        public IMongoClient Cliente  { get; set; }

        public IMongoDatabase Database { get; set; }

        public MongoDbContext(string databaseName)
        {
            this.databaseName = databaseName;
            ConectaBancoDados();
        }

        public IMongoCollection<T> TakeCollection<T>(string collectionName) where T : MongoEntity => Database.GetCollection<T>(collectionName);

        public List<string> TakeCollectionsNames() => Database.ListCollectionNames().ToList<string>();

        public void RemoveCollection(string nome_colecao) => Database.DropCollection(nome_colecao);

        public void RenameCollection(string nome_atual_colecao, string nome_novo_colecao) => Database.RenameCollection(nome_atual_colecao, nome_novo_colecao);

        public void RenameField<T>(
          string nome_colecao,
          Expression<Func<T, bool>> propriedade_filtro,
          string nome_atual_campo,
          string nome_novo_campo)
        {
            IMongoCollection<T> collection = this.Database.GetCollection<T>(nome_colecao);
            UpdateDefinition<T> update = Builders<T>.Update.Rename((FieldDefinition<T>)nome_atual_campo, nome_novo_campo);
            collection.UpdateMany((FilterDefinition<T>)propriedade_filtro, update);
        }

        public void RemoveColumn<T>(
          string nome_colecao,
          Expression<Func<T, object>> propriedade_pesquisa,
          object valor_pesquisa,
          Expression<Func<T, object>> propriedade_remocao)
        {
            Database.GetCollection<T>(nome_colecao).UpdateMany(Builders<T>.Filter.Eq(propriedade_pesquisa, valor_pesquisa), Builders<T>.Update.Unset(propriedade_remocao));
        }

        public bool RemoveDatabase()
        {
            try
            {
                Cliente.DropDatabase(databaseName);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void CriaIndice<T>(
          string nome_colecao,
          Expression<Func<T, object>> propriedade_index_1,
          Expression<Func<T, object>>? propriedade_index_2 = null,
          Expression<Func<T, object>>? propriedade_index_3 = null,
          Expression<Func<T, object>>? propriedade_index_4 = null)
        {
            IMongoCollection<T> collection = Database.GetCollection<T>(nome_colecao);
            IndexKeysDefinition<T>? keys = null;
            if (propriedade_index_4 != null)
                keys = Builders<T>.IndexKeys.Ascending(propriedade_index_1).Ascending<T>(propriedade_index_2).Ascending(propriedade_index_3).Ascending<T>(propriedade_index_4);
            else if (propriedade_index_3 != null)
                keys = Builders<T>.IndexKeys.Ascending(propriedade_index_1).Ascending<T>(propriedade_index_2).Ascending(propriedade_index_3);
            else if (propriedade_index_2 != null)
                keys = Builders<T>.IndexKeys.Ascending(propriedade_index_1).Ascending<T>(propriedade_index_2);
            else if (propriedade_index_1 != null)
                keys = Builders<T>.IndexKeys.Ascending(propriedade_index_1);
            CreateIndexModel<T> model = new CreateIndexModel<T>(keys);
            collection.Indexes.CreateOne(model);
        }

        public void RemoveIndice<T>(string nome_colecao, string nome_indice) => Database.GetCollection<T>(nome_colecao).Indexes.DropOne(nome_indice);

        private void ConectaBancoDados()
        {
            try
            {
                MongoClientSettings settings = new MongoClientSettings();
                settings = Connect();
                Cliente = new MongoClient(settings);
                Database = Cliente.GetDatabase(databaseName);
            }
            catch (Exception)
            {
            }
        }

        private MongoClientSettings Connect() => MongoClientSettings.FromConnectionString("mongodb://localhost:27017?minPoolSize=0&maxPoolSize=100");
    }
}
