using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Interfaces.IService
{
    public interface IBaseService<T> where T : class
    {
        Task<Response<IEnumerable<T>>> ListAllAsync();
        Task<Response<T>> GetByIdAsync(Guid id);
        Response<T> Create(T obj);
        Task<Response<T>> UpdateAsync(T obj);
        Task<Response<T>> Remove(Guid id);
    }
}
