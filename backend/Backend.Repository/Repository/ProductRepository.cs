using Backend.Domain.Entities;
using Backend.Domain.Interfaces.IRepository;
using Backend.Repository.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repository.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {

        public ProductRepository(AppDbContext contexto) : base(contexto)
        {

        }

        public Task<IEnumerable<Product>> GetProductPorPreco()
        {
            throw new NotImplementedException();
        }
    }
}
