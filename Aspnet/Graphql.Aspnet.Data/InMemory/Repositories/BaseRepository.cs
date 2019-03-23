using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Graphql.Aspnet.Core.Data;
using Microsoft.Extensions.Logging;

namespace Graphql.Aspnet.Data.InMemory.Repositories
{
    public abstract class BaseRepository<TEntity, TKey>: IBaseRepository<TEntity, TKey>
        where TEntity: class, IEntity<TKey>, new()
    {
        protected readonly ILogger Logger;
        protected readonly InMemoryContext Db;

        protected BaseRepository(InMemoryContext db, ILogger logger)
        {
            Db = db;
            Logger = logger;
        }

        public virtual Task<List<TEntity>> GetAll()
        {
            var entities = (List<TEntity>) Db.Provider.GetService(typeof(List<TEntity>));
            return Task.FromResult(entities);
        }

        public virtual Task<List<TEntity>> GetAll(string include)
        {
            var entities = (List<TEntity>) Db.Provider.GetService(typeof(List<TEntity>));
            return Task.FromResult(entities);
        }

        public virtual Task<List<TEntity>> GetAll(IEnumerable<string> includes)
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<TEntity> Get(TKey id)
        {
            var entities = (List<TEntity>) Db.Provider.GetService(typeof(List<TEntity>));

            Logger.LogInformation("Get {type} with id = {id}", typeof(TEntity).Name, id);
            return Task.FromResult(entities.SingleOrDefault(c => c.Id.Equals(id)));
        }

        public virtual Task<TEntity> Get(TKey id, string include)
        {
            var entities = (List<TEntity>) Db.Provider.GetService(typeof(List<TEntity>));

            Logger.LogInformation("Get {type} with id = {id}, include {include}", typeof(TEntity).Name, id, include);
            var entity = entities.SingleOrDefault(c => c.Id.Equals(id));
            return Task.FromResult(entity);
        }

        public virtual Task<TEntity> Get(TKey id, IEnumerable<string> includes)
        {
            throw new System.NotImplementedException();
        }

        public virtual TEntity Add(TEntity entity)
        {
            var entities = (List<TEntity>) Db.Provider.GetService(typeof(List<TEntity>));
            entities.Add(entity);
            return entity;
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            throw new System.NotImplementedException();
        }

        public virtual void Delete(TKey id)
        {
            throw new System.NotImplementedException();
        }

        public virtual void Update(TEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<bool> SaveChangesAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}