using BaseLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        T Add(T obj);
        public IEnumerable<T> AddRange(IEnumerable<T> obj);
        Task AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
        //Task Delete<TEntity>(TEntity entity) where TEntity : BaseEntity;
    }
}
