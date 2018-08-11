using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Bjss.PriceBasket.Core.Interfaces
{
    public interface IGenericRepository<T>
    {
        IEnumerable<T> Get(Func<T, bool> expression);
        T GetById(Guid id);
        IEnumerable<T> Get(Func<T, bool> expression, string include);
        IQueryable<T> GetAll(params Expression<Func<T, object>>[] includeExpressions);
        IEnumerable<T> Get<TProperty>(Func<T, bool> expression, Expression<Func<T, TProperty>> include);
        IQueryable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        IQueryable<T> GetAll<TProperty>(Expression<Func<T, TProperty>> include);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> GetAllAsync<TProperty>(Expression<Func<T, bool>> expression, Expression<Func<T, TProperty>> include);
        Task<IEnumerable<T>> GetAllAsync<TProperty>(Expression<Func<T, TProperty>> include);
        void Add(T t);
        void Update(T t);
        void Delete(T t);
        void DeleteById(Guid id);        
    }
}
