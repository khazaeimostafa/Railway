using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Interfaces.Bhrugen;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Bhrugen
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly StoreContext _context;

        internal DbSet<T> dbSet; // { get; set; }

        public Repository(StoreContext context)
        {
            _context = context;

            dbSet = context.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add (entity);
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            IQueryable<T> query = dbSet;

            if (spec.Criteria != null) query = query.Where(spec.Criteria);
            return await query.CountAsync();
        }

        public T Get(int id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<T>
        GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null
        )
        {
            IQueryable<T> query = dbSet;
            if (filter != null) query = query.Where(filter);

            if (includeProperties != null)
            {
                foreach (var
                    includePropertie
                    in
                    includeProperties
                        .Split(new char[] { ',' },
                        StringSplitOptions.RemoveEmptyEntries)
                )
                {
                    query = query.Include(includePropertie);
                }
            }

            if (orderBy != null) return (orderBy(query).ToList());
            return query.ToList();
        }

        public async Task<IEnumerable<T>>
        GetEntityWithSpec(ISpecification<T> spec)
        {
            IQueryable<T> query = dbSet;

            if (spec.Criteria != null) query = query.Where(spec.Criteria);
            if (spec.OrderBy != null) query = query.OrderBy(spec.OrderBy);
            if (spec.OrderByDescending != null)
                query = query.OrderByDescending(spec.OrderByDescending);

            if (spec.IsPagingEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

            query =
                spec
                    .Includes
                    .Aggregate(query,
                    (current, include) => current.Include(include));

            return await query.AsQueryable().ToListAsync();
        }

        public async Task<T> GetFirstOrDefault(ISpecification<T> spec)
        {
            IQueryable<T> query = dbSet;
            if (spec.Criteria != null) query = query.Where(spec.Criteria);
            if (spec.OrderBy != null) query = query.OrderBy(spec.OrderBy);
            if (spec.OrderByDescending != null)
                query = query.OrderByDescending(spec.OrderByDescending);

            if (spec.IsPagingEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

            query =
                spec
                    .Includes
                    .Aggregate(query,
                    (current, include) => current.Include(include));

            return await query.AsQueryable().FirstOrDefaultAsync();
        }

        public void Remove(int id)
        {
            T entityToRemove = dbSet.Find(id);
            Remove (entityToRemove);
        }

        public void Remove(T entity)
        {
            dbSet.Remove (entity);
        }
    }
}
