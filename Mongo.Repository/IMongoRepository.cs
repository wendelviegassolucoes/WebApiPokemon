using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mongo.Repository
{
    public interface IMongoRepository<T> where T : MongoEntity
    {
        IMongoDbContext Context { get; }

        IEnumerable<T> TakeList(
            Expression<Func<T, bool>>? filterProperties = null,
            Expression<Func<T, T>>? selection = null,
            int skipElements = 0,
            int returnElements = 0);

        Task<IEnumerable<T>> TakeListAsync(
          Expression<Func<T, bool>>? filter = null,
          Expression<Func<T, T>>? selection = null,
          int returnElements = 0);

        void Insert(T entity, IClientSessionHandle? session = null);

        Task InsertAsync(T entity, IClientSessionHandle? session = null);

        void InsertMany(IEnumerable<T> entitys, IClientSessionHandle? session = null);

        Task InsertManyAsync(IEnumerable<T> entitys, IClientSessionHandle? session = null);

        void InsertManyArray<TItem>(
          Expression<Func<T, object>>? filterProperties,
          object? filterValue,
          Expression<Func<T, IEnumerable<TItem>>>? insertProperty,
          IEnumerable<TItem>? valuesInsert,
          IClientSessionHandle? session = null);

        void InsertArray<TItem>(
          Expression<Func<T, bool>> filterProperties,
          Expression<Func<T, IEnumerable<TItem>>> insertProperty,
          TItem insertValue,
          IClientSessionHandle? session = null);

        void InsertArrayDeep<TItem>(
          Expression<Func<T, bool>> filterProperties,
          List<ArrayFilterDefinition> arrayFilters,
          string updateProperty,
          TItem insertValue,
          IClientSessionHandle? session = null);

        void Update(T entity, IClientSessionHandle? session = null);

        Task UpdateAsync(T entity, IClientSessionHandle? session = null);

        void UpdateMany(IEnumerable<T> entitys, IClientSessionHandle? session = null);

        void Update(
          Expression<Func<T, bool>> filterProperty,
          Expression<Func<T, object>> updateProperty,
          object updateValue,
          IClientSessionHandle? session = null);

        Task UpdateAsync(
          Expression<Func<T, bool>> filterProperty,
          Expression<Func<T, object>> updateProperty,
          object updateValue,
          IClientSessionHandle? session = null);

        void UpdateArray<TItem, TField>(
          Expression<Func<T, object>> filterProperty,
          object filterValue,
          Expression<Func<T, IEnumerable<TItem>>> filterArrayProperties,
          Expression<Func<TItem, bool>> updateArrayFilter,
          Expression<Func<T, object>> updateProperty,
          TField updateValue,
          IClientSessionHandle? session = null);

        void UpdateArrayDeep<TField>(
          Expression<Func<T, bool>> filterProperty,
          List<ArrayFilterDefinition> arrayFilter,
          string updateProperty,
          TField updateValue,
          IClientSessionHandle? session = null);

        void Remove(Expression<Func<T, bool>> filterProperty, IClientSessionHandle? session = null);

        void RemoveMany(Expression<Func<T, bool>> filterProperty, IClientSessionHandle? session = null);

        Task RemoveManyAsync(
          Expression<Func<T, bool>> filterProperty,
          IClientSessionHandle? session = null);

        void RemoveArray<TItem>(
          Expression<Func<T, bool>> filterProperty,
          Expression<Func<T, IEnumerable<TItem>>> removeProperty,
          Expression<Func<TItem, bool>> filterArray,
          IClientSessionHandle? session = null);

        void RemoveArrayDeep(
          Expression<Func<T, bool>> filterProperty,
          string removeProperty,
          string arrayFilterProperty,
          object arrayFilterValue,
          IClientSessionHandle? session = null);
    }
}
