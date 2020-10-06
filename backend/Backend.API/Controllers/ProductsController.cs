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

namespace Backend.API.Controllers
{
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
        public async Task<IEnumerable<ProductDTO>> GetProduct()
        {
            var product = await _productService.ListAllAsync();
            var productDTO = _mapper.Map<IEnumerable<ProductDTO>>(product);
            return productDTO;
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProductById(Guid id)
        {

            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            var productDTO = _mapper.Map<ProductDTO>(product);

            return productDTO;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(Guid id, ProductDTO productDTO)
        {
            if (id != productDTO.Id)
            {
                return BadRequest();
            }

            var product = _mapper.Map<Product>(productDTO);

            await _productService.UpdateAsync(product);
            return Ok();
        }

        // POST: api/Products
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public ActionResult<Product> PostProduct(ProductDTO productDTO)
        {
            var product = _mapper.Map<Product>(productDTO);
            _productService.Create(product);
            return Ok();
          
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductDTO>> DeleteProduct(Guid id)
        {
            await _productService.Remove(id);
            return Ok();
        }
    }
}
