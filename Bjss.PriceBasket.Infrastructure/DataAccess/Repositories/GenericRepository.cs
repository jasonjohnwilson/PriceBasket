using Bjss.PriceBasket.Core.Interfaces;
using Bjss.PriceBasket.Core.Models;
using Bjss.PriceBasket.Infrastructure.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Bjss.PriceBasket.Infrastructure.DataAccess.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : Entity
    {
        private readonly DbContext _context;

        public GenericRepository(PriceBasketContext context)
        {
            _context = context;
        }

        public IEnumerable<T> Get(Func<T, bool> expression)
        {
            return _context.Set<T>().Where(expression);
        }

        public T GetById(Guid id)
        {
            return _context.Set<T>().SingleOrDefault(t => t.Id == id);
        }

        public IEnumerable<T> Get(Func<T, bool> expression, string include)
        {
            return _context.Set<T>().Include(include).Where(expression);
        }

        public IQueryable<T> GetAll(params Expression<Func<T, object>>[] includeExpressions)
        {
            return includeExpressions
                .Aggregate<Expression<Func<T, object>>, IQueryable<T>>(_context.Set<T>(), (current, expression) => current.Include(expression));
        }

        public IEnumerable<T> Get<TProperty>(Func<T, bool> expression, Expression<Func<T, TProperty>> include)
        {
            return _context.Set<T>().Include(include).Where(expression);
        }

        public virtual IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public virtual IQueryable<T> GetAll<TProperty>(Expression<Func<T, TProperty>> include)
        {
            return _context.Set<T>().Include(include);
        }

        public async virtual Task<IEnumerable<T>> GetAllAsync<TProperty>(Expression<Func<T, bool>> expression, Expression<Func<T, TProperty>> include)
        {
            return await _context.Set<T>()
                                .Where(expression)
                                .Include(include)
                                .ToArrayAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>()
                                .Where(expression)
                                .ToArrayAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync<TProperty>(Expression<Func<T, TProperty>> include)
        {
            return await _context.Set<T>()
                                .Include(include)
                                .ToArrayAsync();
        }

        public void Add(T t)
        {
            EntityEntry entry = _context.Entry(t);

            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                _context.Set<T>().Add(t);
            }
        }

        public void Update(T t)
        {
            EntityEntry entry = _context.Entry(t);

            if (entry.State == EntityState.Detached)
            {
                _context.Set<T>().Attach(t);
            }

            entry.State = EntityState.Modified;
        }

        public void Delete(T t)
        {
            EntityEntry entry = _context.Entry(t);

            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                _context.Set<T>().Attach(t);
                _context.Set<T>().Remove(t);
            }
        }

        public void DeleteById(Guid id)
        {
            T t = GetById(id);

            Delete(t);
        }
    }
}
