using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using graphql.core.Data;
using Microsoft.Extensions.Logging;

namespace graphql.data.InMemory
{
    public abstract class BaseRepository<TEntity, TKey>: IBaseRepository<TEntity, TKey>
        where TEntity: class, IEntity<TKey>, new()
    {
        protected List<TEntity> Entities = new List<TEntity>();
        protected readonly ILogger Logger;

        protected BaseRepository(ILogger logger)
        {
            Logger = logger;
        }

        public virtual Task<List<TEntity>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<List<TEntity>> GetAll(string include)
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<List<TEntity>> GetAll(IEnumerable<string> includes)
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<TEntity> Get(TKey id)
        {
            Logger.LogInformation("Get {type} with id = {id}", typeof(TEntity).Name, id);
            return Task.FromResult(Entities.FirstOrDefault());
        }

        public virtual Task<TEntity> Get(TKey id, string include)
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<TEntity> Get(TKey id, IEnumerable<string> includes)
        {
            throw new System.NotImplementedException();
        }

        public virtual TEntity Add(TEntity entity)
        {
            throw new System.NotImplementedException();
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