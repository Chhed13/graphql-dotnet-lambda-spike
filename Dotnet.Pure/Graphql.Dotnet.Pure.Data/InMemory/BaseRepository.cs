using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using graphql.core.Data;
using Microsoft.Extensions.Logging;

namespace graphql.data.InMemory
{
    public abstract class BaseRepository<TEntity, Tkey>: IBaseRepository<TEntity, Tkey>
        where TEntity: class, IEntity<Tkey>, new()
    {
        protected List<TEntity> _entities = new List<TEntity>();
        protected readonly ILogger _logger;

        protected BaseRepository(ILogger logger)
        {
            _logger = logger;
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

        public virtual Task<TEntity> Get(Tkey id)
        {
            _logger.LogInformation("Get {type} with id = {id}", typeof(TEntity).Name, id);
            return Task.FromResult(_entities.FirstOrDefault());
        }

        public virtual Task<TEntity> Get(Tkey id, string include)
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<TEntity> Get(Tkey id, IEnumerable<string> includes)
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

        public virtual void Delete(Tkey id)
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