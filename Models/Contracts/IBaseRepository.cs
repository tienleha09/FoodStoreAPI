using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FoodStoreAPI.Models.Contracts
{
    public interface IBaseRepository<T> where T : class
    {
        IQueryable<T> FindAll(bool trackChanges);
        T FindById(int id);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
