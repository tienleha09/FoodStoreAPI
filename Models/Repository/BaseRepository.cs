using FoodStoreAPI.Models.Contracts;
using FoodStoreAPI.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace FoodStoreAPI.Models.Repository
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly DataContext _dataContext;
        public BaseRepository(DataContext context)
        {
            _dataContext = context;
        }

        public void Create(T entity)
        {
            _dataContext.Set<T>().Add(entity);           
        }

        public void Delete(T entity)
        {
            _dataContext.Set<T>().Remove(entity);            
        }

        public IQueryable<T> FindAll(bool trackChanges)
        {
            return trackChanges? _dataContext.Set<T>() : _dataContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
        {
            return trackChanges ? _dataContext.Set<T>().Where(expression)
                                : _dataContext.Set<T>().Where(expression).AsNoTracking();
        }

        public T FindById(int id)
        {
            return _dataContext.Set<T>().Find(id);
        }

        public void Update(T entity)
        {
           _dataContext.Entry<T>(entity).State = EntityState.Modified ;
        }
    }
}
