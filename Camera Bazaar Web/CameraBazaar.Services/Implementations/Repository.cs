namespace CameraBazaar.Services.Implementations
{
    using Interfaces;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private DbSet<TEntity> dbSet;

        public Repository(DbSet<TEntity> dbSet)
        {
            this.dbSet = dbSet;

        }

        public void AddAsync(TEntity entity)
        {
            Task.Run(async () =>
            {
                await this.dbSet.AddAsync(entity);
            })
            .Wait();
        }


        public void Remove(TEntity entity)
        {
            this.dbSet.Remove(entity);
        }

        public TEntity FindById(int? id)
        {
            TEntity entity = default(TEntity);

            Task.Run(async () =>
                {
                    entity = await this.dbSet.FindAsync(id);
                })
               .Wait();

            if (entity == null)
            {
                // return here null entity
            }

            return entity;

        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression)
        {
            TEntity entity = default(TEntity);

            Task.Run(async () =>
                {
                    entity = await this.dbSet.FirstOrDefaultAsync(expression);
                })
                .Wait();

            if (entity == null)
            {
                // return here null entity
            }

            return entity;
        }

        public bool Any(Expression<Func<TEntity, bool>> expression)
        {
            bool any = false;

            Task.Run(async () =>
                {
                    any = await this.dbSet.AnyAsync(expression);
                })
                .Wait();

            return any;
        }

        public IEnumerable<TEntity> GetAll()
        {
            List<TEntity> getEntities = new List<TEntity>();

            Task.Run(async () =>
               {
                   getEntities = await this.dbSet.ToListAsync();
               })
                .Wait();

            if (getEntities == null)
            {
                // return here null entity
            }

            return getEntities;
        }

        public IEnumerable<TEntity> Filter(Expression<Func<TEntity, bool>> expression)
        {
            return this.dbSet.Where(expression);
        }

        public IEnumerable<TEntity> OrderBy(Expression<Func<TEntity, bool>> expression)
        {
            return this.dbSet.OrderBy(expression);
        }

        public int Count()
        {
            return this.dbSet.Count();
        }

        public int Count(Expression<Func<TEntity, bool>> expression)
        {
            return this.dbSet.Count(expression);
        }
    }
}
