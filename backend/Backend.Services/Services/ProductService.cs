using Backend.Domain.Entities;
using Backend.Domain.Interfaces.IRepository;
using Backend.Domain.Interfaces.IService;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Services.Services
{
    public class ProductService : IProductService
    {

        protected readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public ProductService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public Product Create(Product obj)
        {
            obj.DateCreated = DateTime.Now;
            obj.DateUpdate = DateTime.Now;

            var product =_unitOfWork.ProductRepository.Create(obj);
            _unitOfWork.Commit();
            return product;
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            return await _unitOfWork.ProductRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Product>> ListAllAsync()
        {
            return await _unitOfWork.ProductRepository.ListAllAsync();
        }

        public async Task<Product> Remove(Guid id)
        {
            var entity = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (entity != null)
            {
               _unitOfWork.ProductRepository.Remove(entity);
                _unitOfWork.Commit();
            }
            return entity;
        }

        public async Task<Product> UpdateAsync(Product obj)
        {
            var entity = await _unitOfWork.ProductRepository.GetByIdAsync(obj.Id);

            entity.Name = obj.Name;
            entity.Price = obj.Price;
            entity.DateUpdate = DateTime.Now;

            _unitOfWork.ProductRepository.UpdateAsync(entity);
            _unitOfWork.Commit();

            return entity;
        }
    }
}
