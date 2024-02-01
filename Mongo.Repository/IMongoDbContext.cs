using System.Linq.Expressions;
using MongoDB.Driver;

namespace Mongo.Repository
{
    public interface IMongoDbContext
    {
        IMongoClient Cliente { get; set; }

        IMongoCollection<T> TakeCollection<T>(string nome_colecao) where T : MongoEntity;

        List<string> TakeCollectionsNames();

        void RemoveCollection(string nome_colecao);

        void RenameCollection(string nome_atual_colecao, string nome_novo_colecao);

        void RenameField<T>(
            string nome_colecao,
            Expression<Func<T, bool>> propriedade_filtro,
            string nome_atual_campo,
            string nome_novo_campo);

        void RemoveColumn<T>(
            string nome_colecao,
            Expression<Func<T, object>> propriedade_pesquisa,
            object valor_pesquisa,
            Expression<Func<T, object>> propriedade_remocao);

        bool RemoveDatabase();

        void CriaIndice<T>(
            string nome_colecao,
            Expression<Func<T, object>> propriedade_index_1,
            Expression<Func<T, object>> propriedade_index_2,
            Expression<Func<T, object>> propriedade_index_3,
            Expression<Func<T, object>> propriedade_index_4);

        void RemoveIndice<T>(string nome_colecao, string nome_indice);
    }
}