using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Domain.Interfaces.IRepository
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepository { get; }

        void Commit();
    }
}
