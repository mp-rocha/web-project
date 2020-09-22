using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Interfaces.IService
{
    public interface IBaseService<T> where T : class
    {
        Task<IEnumerable<T>> ListAllAsync();
        Task<T> GetByIdAsync(Guid id);
        T Create(T obj);
        Task<T> UpdateAsync(T obj);
        Task<T> Remove(Guid id);
    }
}
