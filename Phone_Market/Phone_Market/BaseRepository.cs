using Phone_Market.Models;

namespace Phone_Market
{
    
        public class BaseRepository<TEntity> : IRepository<TEntity>
            where TEntity : class, IEntity
        {
            private readonly Phone_MarketContext Context;

            public BaseRepository(Phone_MarketContext context)
            {
                this.Context = context;
            }

            public IQueryable<TEntity> Get()
            {
                return Context.Set<TEntity>().AsQueryable();
            }

            public TEntity Insert(TEntity entity)
            {
                Context.Set<TEntity>().Add(entity);
                return entity;
            }

            public TEntity Update(TEntity entitty)
            {
                Context.Set<TEntity>().Update(entitty);

                return entitty;
            }

            public void Delete(TEntity entity)
            {
                Context.Set<TEntity>().Remove(entity);
            }
        }
    }


