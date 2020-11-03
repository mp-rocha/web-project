using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Domain.Entities;
using Backend.Repository.Context;
using Backend.Services.Services;
using Backend.Domain.Interfaces.IService;
using AutoMapper;
using Backend.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace Backend.API.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(AppDbContext context, IProductService productService, IMapper mapper)
        {
            _context = context;
            _productService = productService;
            _mapper = mapper;
        }

        // GET: api/Products 
        [HttpGet]
        public async Task<Response<IEnumerable<ProductDTO>>> GetProduct()
        {
            var product = await _productService.ListAllAsync();
            var productDTO = _mapper.Map<IEnumerable<ProductDTO>>(product.Data);;
            return Response<IEnumerable<ProductDTO>>.GetResult(product.Code, product.Msg, productDTO);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<Response<ProductDTO>> GetProductById(Guid id)
        {
            var product = await _productService.GetByIdAsync(id);
            var productDTO = _mapper.Map<ProductDTO>(product.Data);
            return Response<ProductDTO>.GetResult(product.Code, product.Msg, productDTO);
        }

        // POST: api/Products
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public Response<Product> PostProduct(ProductDTO productDTO)
        {
            var product = _productService.Create(_mapper.Map<Product>(productDTO));
            return Response<Product>.GetResult(product.Code, product.Msg, product.Data);
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        public async Task<Response<Product>> PutProduct(ProductDTO productDTO)
        {
            var product = await _productService.UpdateAsync(_mapper.Map<Product>(productDTO));
            return Response<Product>.GetResult(product.Code, product.Msg, product.Data);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<Response<Product>> DeleteProduct(Guid id)
        {
            var product = await _productService.Remove(id);
            return Response<Product>.GetResult(product.Code, product.Msg, product.Data);
        }
    }
}
