namespace CameraBazaar.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;


    public interface IRepository<TEntity> where TEntity : class
    {
        void AddAsync(TEntity entity);

        void Remove(TEntity entity);

        TEntity FindById(int id);

        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression);

        bool Any(Expression<Func<TEntity, bool>> expression);

        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> Filter(Expression<Func<TEntity, bool>> expression);

        IEnumerable<TEntity> OrderBy(Expression<Func<TEntity, bool>> expression);

        int Count();

        int Count(Expression<Func<TEntity, bool>> expression);
        
    }
}
