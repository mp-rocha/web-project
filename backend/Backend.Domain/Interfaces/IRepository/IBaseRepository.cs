using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Interfaces.IRepository
{
    public interface IBaseRepository<T> where T : class
    {
        Task<List<T>> ListAllAsync();
        Task<T> GetByIdAsync(Guid id);
        T Create(T entity);
        void UpdateAsync(T entity);
        void Remove(T entity);
    }
}
