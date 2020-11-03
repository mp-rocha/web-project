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

        public Response<Product> Create(Product obj)
        {
            obj.Id = Guid.NewGuid();
            obj.DateCreated = DateTime.Now;
            obj.DateUpdate = DateTime.Now;

            var product =_unitOfWork.ProductRepository.Create(obj);
            _unitOfWork.Commit();
            return Response<Product>.GetResult(200, "OK", product);
        }

        public async Task<Response<Product>> GetByIdAsync(Guid id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);

            if (product != null)
            {
                return Response<Product>.GetResult(200, "OK", product);
            }
            else
            {
                return Response<Product>.GetResult(200, "Não existe produto com o ID informado!", null);
            }
            
        }

        public async Task<Response<IEnumerable<Product>>> ListAllAsync()
        {
            var products = await _unitOfWork.ProductRepository.ListAllAsync();

            if (products.Count != 0)
            {
                return Response<IEnumerable<Product>>.GetResult(200, "OK", products);
            }
            else
            {
                return Response<IEnumerable<Product>>.GetResult(200, "Não há registros na base de dados!", products);
            }
            
        }

        public async Task<Response<Product>> Remove(Guid id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);

            if (product != null)
            {
                _unitOfWork.ProductRepository.Remove(product);
                _unitOfWork.Commit();
                return Response<Product>.GetResult(200, "OK", product);
            }
            else
            {
                return Response<Product>.GetResult(200, "Não existe produto com o ID informado!", product);
            }
        }

        public async Task<Response<Product>> UpdateAsync(Product obj)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(obj.Id);

            if (product != null)
            {
                product.Name = obj.Name;
                product.Price = obj.Price;
                product.DateUpdate = DateTime.Now;

                _unitOfWork.ProductRepository.UpdateAsync(product);
                _unitOfWork.Commit();

                return Response<Product>.GetResult(200, "OK", product);
            }
            else
            {
                return Response<Product>.GetResult(200, "Não existe produto com o ID informado!", product);
            }
        }
    }
}
