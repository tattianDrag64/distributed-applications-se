using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientLibrary.Services.Interfaces
{
    public interface IBaseManager<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<string> Add(T entity);
        Task Update(T entity);
        Task DeleteAsync(int id);
    }
}
