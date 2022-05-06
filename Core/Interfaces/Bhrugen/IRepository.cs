using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Specifications;

namespace Core.Interfaces.Bhrugen
{
    public interface IRepository<T> where T : class
    { 
        T Get(int id);

        IEnumerable<T>
        GetAll(

                Expression<Func<T, bool>> filter = null,
                Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                string includeProperties = null

        );

        Task<T> GetFirstOrDefault(ISpecification<T> spec);

        Task<IEnumerable<T>> GetEntityWithSpec(ISpecification<T> spec);

        Task<int> CountAsync(ISpecification<T> spec);

        void Add(T entity);

        void Remove(int id);

        void Remove(T entity);
    }
}
