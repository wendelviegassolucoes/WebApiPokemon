using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq.Expressions;

namespace Mongo.Repository
{
    public class MongoRepository<T> : IMongoRepository<T> where T : MongoEntity
    {
        private readonly IMongoDbContext context;
        private IMongoCollection<T> collection;
        private string collectionName;

        private IMongoCollection<T> Collection
        {
            get
            {
                if (collection == null)
                    collection = context.TakeCollection<T>(collectionName);
                return collection;
            }
        }

        public IMongoDbContext Context => context;


        public MongoRepository(string databaseName, string collectionName)
        {
            this.collectionName = collectionName;
            context = new MongoDbContext(databaseName);
        }

        public IEnumerable<T> TakeList(
        Expression<Func<T, bool>>? filterProperties = null,
        Expression<Func<T, T>>? selection = null,
        int skipElements = 0,
        int returnElements = 0)
        {
            IMongoQueryable<T> source;

            if (filterProperties == null && selection == null)
                source = Collection.AsQueryable();
            else if (filterProperties == null && selection != null)
                source = Collection.AsQueryable().Select(selection);
            else if (filterProperties != null && selection == null)
                source = Collection.AsQueryable().Where(filterProperties);
            else
            {
                if (!(filterProperties != null && selection != null))
                    throw new NullReferenceException();
                source = Collection.AsQueryable().Where(filterProperties).Select(selection);
            }

            if (skipElements > 0)
                source = source.Skip(skipElements);

            return returnElements == 0 ? source.ToList() : source.Take(returnElements).ToList() as IEnumerable<T>;
        }

        public async Task<IEnumerable<T>> TakeListAsync(
          Expression<Func<T, bool>>? filterProperties = null,
          Expression<Func<T, T>>? selection = null,
          int returnElements = 0)
        {
            IMongoQueryable<T> query;

            if (filterProperties == null & selection == null)
                query = Collection.AsQueryable();
            else if (filterProperties == null & selection != null)
                query = Collection.AsQueryable().Select(selection);
            else if (filterProperties != null & selection == null)
            {
                query = Collection.AsQueryable().Where(filterProperties);
            }
            else
            {
                if (!(filterProperties != null & selection != null))
                    throw new NullReferenceException();
                query = Collection.AsQueryable().Where<T>(filterProperties).Select(selection);
            }
            if (returnElements == 0)
            {
                List<T> listAsync = await query.ToListAsync<T>();
                return listAsync;
            }
            List<T> listAsync1 = await query.Take(returnElements).ToListAsync<T>();
            return listAsync1;
        }

        public void InsertMany(IEnumerable<T> documents, IClientSessionHandle? session = null)
        {
            if (documents == null)
                throw new NullReferenceException(nameof(documents));
            if (session == null)
                Collection.InsertMany(documents);
            else
                Collection.InsertMany(session, documents);
        }

        public async Task InsertManyAsync(IEnumerable<T> documents, IClientSessionHandle? session = null)
        {
            if (documents == null)
                throw new NullReferenceException(nameof(documents));
            if (session == null)
                await Collection.InsertManyAsync(documents);
            else
                await Collection.InsertManyAsync(session, documents);
        }

        public void Insert(T documento, IClientSessionHandle? session = null)
        {
            if (documento == null)
                throw new NullReferenceException(nameof(documento));
            if (session == null)
                Collection.InsertOne(documento);
            else
                Collection.InsertOne(session, documento);
        }

        public async Task InsertAsync(T document, IClientSessionHandle? session = null)
        {
            if (document == null)
                throw new NullReferenceException(nameof(document));
            if (session == null)
                await Collection.InsertOneAsync(document);
            else
                await Collection.InsertOneAsync(session, document);
        }

        public void InsertManyArray<TItem>(
          Expression<Func<T, object>> filterProperties,
          object? valueFilter,
          Expression<Func<T, IEnumerable<TItem>>>? propriedade_insercao,
          IEnumerable<TItem>? valuesInsert,
          IClientSessionHandle? session = null)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Eq(filterProperties!, valueFilter);
            UpdateDefinition<T> update = Builders<T>.Update.PushEach(propriedade_insercao, valuesInsert);
            if (session == null)
                Collection.UpdateOne(filter, update);
            else
                Collection.UpdateOne(session, filter, update);
        }

        public void InsertArray<TItem>(
          Expression<Func<T, bool>> propriedade_filtro,
          Expression<Func<T, IEnumerable<TItem>>> propriedade_insercao,
          TItem valor_insercao,
          IClientSessionHandle? session = null)
        {
            UpdateDefinition<T> update = Builders<T>.Update.Push(propriedade_insercao, valor_insercao);
            if (session == null)
                Collection.UpdateOne((FilterDefinition<T>)propriedade_filtro, update);
            else
                Collection.UpdateOne(session, (FilterDefinition<T>)propriedade_filtro, update);
        }

        public void InsertArrayDeep<TItem>(
          Expression<Func<T, bool>> propriedade_filtro,
          List<ArrayFilterDefinition> filtros_array,
          string propriedade_atualizacao,
          TItem valor_insercao,
          IClientSessionHandle? session = null)
        {
            UpdateDefinition<T> update = Builders<T>.Update.Push((FieldDefinition<T>)propriedade_atualizacao, valor_insercao);
            UpdateOptions options = new UpdateOptions()
            {
                ArrayFilters = (IEnumerable<ArrayFilterDefinition>)filtros_array
            };
            if (session == null)
                Collection.UpdateOne((FilterDefinition<T>)propriedade_filtro, update, options);
            else
                Collection.UpdateOne(session, (FilterDefinition<T>)propriedade_filtro, update, options);
        }

        public void Update(T document, IClientSessionHandle? session = null)
        {
            if (document == null)
                throw new NullReferenceException(nameof(document));
            FilterDefinition<T> filter = Builders<T>.Filter.Eq((Expression<Func<T, string>>)(x => x.Id), document.Id);
            if (session == null)
                Collection.ReplaceOne(filter, document);
            else
                Collection.ReplaceOne(session, filter, document);
        }

        public async Task UpdateAsync(T document, IClientSessionHandle? session = null)
        {
            if (document == null)
                throw new NullReferenceException(nameof(document));
#pragma warning disable CS8620 // O argumento não pode ser usado para o parâmetro devido a diferenças na nulidade dos tipos de referência.
            FilterDefinition<T>? filter = Builders<T>.Filter.Eq((Expression<Func<T, string>>)(x => x.Id), document.Id);
#pragma warning restore CS8620 // O argumento não pode ser usado para o parâmetro devido a diferenças na nulidade dos tipos de referência.
            if (session == null)
            {
                ReplaceOneResult replaceOneResult = await Collection.ReplaceOneAsync(filter, document);
                filter = null;
            }
            else
            {
                ReplaceOneResult replaceOneResult = await Collection.ReplaceOneAsync(session, filter, document);
                filter = null;
            }
        }

        public void UpdateMany(IEnumerable<T> documents, IClientSessionHandle? session = null)
        {
            if (documents == null)
                throw new NullReferenceException(nameof(documents));
            foreach (T document in documents)
                Update(document, null);
        }

        public void Update(
          Expression<Func<T, bool>> propriedade_filtro,
          Expression<Func<T, object>> propriedade_atualizacao,
          object valor_atualizacao,
          IClientSessionHandle? session = null)
        {
            UpdateDefinition<T> update = Builders<T>.Update.Set(propriedade_atualizacao, valor_atualizacao);
            if (session == null)
                Collection.UpdateMany((FilterDefinition<T>)propriedade_filtro, update);
            else
                Collection.UpdateMany(session, (FilterDefinition<T>)propriedade_filtro, update);
        }

        public async Task UpdateAsync(
          Expression<Func<T, bool>> propriedade_filtro,
          Expression<Func<T, object>> propriedade_atualizacao,
          object valor_atualizacao,
          IClientSessionHandle? session = null)
        {
            UpdateDefinition<T>? update = Builders<T>.Update.Set(propriedade_atualizacao, valor_atualizacao);
            if (session == null)
            {
                UpdateResult updateResult = await Collection.UpdateManyAsync((FilterDefinition<T>)propriedade_filtro, update);
            }
            else
            {
                UpdateResult updateResult = await Collection.UpdateManyAsync(session, (FilterDefinition<T>)propriedade_filtro, update);
            }
        }

        public void UpdateArray<TItem, TField>(
          Expression<Func<T, object>> propriedade_filtro,
          object valor_filtro,
          Expression<Func<T, IEnumerable<TItem>>> propriedade_array_filtro,
          Expression<Func<TItem, bool>> filtro_array_atualizacao,
          Expression<Func<T, object>> propriedade_atualizacao,
          TField valor_atualizacao,
          IClientSessionHandle? session = null)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.And((IEnumerable<FilterDefinition<T>>)new List<FilterDefinition<T>>()
      {
        Builders<T>.Filter.Eq(propriedade_filtro, valor_filtro),
        Builders<T>.Filter.ElemMatch(propriedade_array_filtro, filtro_array_atualizacao)
      });
#pragma warning disable CS8604 // Possível argumento de referência nula.
            UpdateDefinition<T> update = Builders<T>.Update.Set(propriedade_atualizacao, valor_atualizacao);
#pragma warning restore CS8604 // Possível argumento de referência nula.
            if (session == null)
                Collection.UpdateOne(filter, update);
            else
                Collection.UpdateOne(session, filter, update);
        }

        public void UpdateArrayDeep<TField>(
          Expression<Func<T, bool>> propriedade_filtro,
          List<ArrayFilterDefinition> filtros_array,
          string propriedade_atualizacao,
          TField valor_atualizacao,
          IClientSessionHandle? session = null)
        {
            UpdateDefinition<T> update = Builders<T>.Update.Set<TField>((FieldDefinition<T, TField>)propriedade_atualizacao, valor_atualizacao);
            UpdateOptions options = new UpdateOptions()
            {
                ArrayFilters = (IEnumerable<ArrayFilterDefinition>)filtros_array
            };
            if (session == null)
                Collection.UpdateOne((FilterDefinition<T>)propriedade_filtro, update, options);
            else
                Collection.UpdateOne(session, (FilterDefinition<T>)propriedade_filtro, update, options);
        }

        public void Remove(Expression<Func<T, bool>> propriedade_filtro, IClientSessionHandle? session = null)
        {
            if (session == null)
                Collection.DeleteOne((FilterDefinition<T>)propriedade_filtro);
            else
                Collection.DeleteOne(session, (FilterDefinition<T>)propriedade_filtro);
        }

        public async Task RemoveAsync(Expression<Func<T, bool>> filterProperty, IClientSessionHandle? session = null)
        {
            if (session == null)
                await Collection.DeleteOneAsync((FilterDefinition<T>)filterProperty);
            else
                await Collection.DeleteOneAsync(session, (FilterDefinition<T>)filterProperty);
        }

        public void RemoveMany(Expression<Func<T, bool>> filterProperty, IClientSessionHandle? session = null)
        {
            if (session == null)
                Collection.DeleteMany((FilterDefinition<T>)filterProperty);
            else
                Collection.DeleteMany(session, (FilterDefinition<T>)filterProperty);
        }

        public async Task RemoveManyAsync(
          Expression<Func<T, bool>> filterProperty,
          IClientSessionHandle? session = null)
        {
            if (session == null)
            {
                DeleteResult deleteResult1 = await Collection.DeleteManyAsync((FilterDefinition<T>)filterProperty);
            }
            else
            {
                DeleteResult deleteResult2 = await Collection.DeleteManyAsync(session, (FilterDefinition<T>)filterProperty);
            }
        }

        public void RemoveArray<TItem>(
          Expression<Func<T, bool>> filterProperty,
          Expression<Func<T, IEnumerable<TItem>>> removeProperty,
          Expression<Func<TItem, bool>> filterArray,
          IClientSessionHandle? session = null)
        {
            UpdateDefinition<T> update = Builders<T>.Update.PullFilter(removeProperty, filterArray);
            if (session == null)
                Collection.UpdateOne((FilterDefinition<T>)filterProperty, update);
            else
                Collection.UpdateOne(session, (FilterDefinition<T>)filterProperty, update);
        }

        public void RemoveArrayDeep(
            Expression<Func<T, bool>> filterProperty,
            string removeProperty,
            string arrayFilterProperty,
            object arrayFilterValue,
            IClientSessionHandle? session = null)
        {
            if (arrayFilterProperty == "_id")
                arrayFilterValue = ObjectId.Parse(arrayFilterValue.ToString());
            UpdateDefinition<T> update = Builders<T>.Update.PullFilter<T>((FieldDefinition<T>)removeProperty, Builders<T>.Filter.Eq((FieldDefinition<T, object>)arrayFilterProperty, arrayFilterValue));
            if (session == null)
                Collection.UpdateOne((FilterDefinition<T>)filterProperty, update);
            else
                Collection.UpdateOne(session, (FilterDefinition<T>)filterProperty, update);
        }
    }
}
