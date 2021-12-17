using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CleanArch.Application.DTOs;
using CleanArch.Application.Interfaces;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces;

namespace CleanArch.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            return _mapper.Map<IEnumerable<ProductDTO>>(await _repository.GetProductsAsync());
        }

        public async Task<ProductDTO> GetProductById(int? id)
        {
            if (!id.HasValue) return null;
            return _mapper.Map<ProductDTO>(await _repository.GetByIdAsync(id));
        }

        public async Task<ProductDTO> GetProductCategory(int? id)
        {
            if (!id.HasValue) return null;
            return _mapper.Map<ProductDTO>(await _repository.GetProductByCategoryAsync(id));
        }

        public async Task Add(ProductDTO productDto)
        {
            var productEntity = _mapper.Map<Product>(productDto);
            await _repository.CreateAsync(productEntity);
        }

        public async Task Update(ProductDTO productDto)
        {
            var productEntity = _mapper.Map<Product>(productDto);
            await _repository.UpdateAsync(productEntity);
        }

        public async Task Remove(int? id)
        {
            if (!id.HasValue) return;
            var productEntity = await _repository.GetByIdAsync(id);
            await _repository.RemoveAsync(productEntity);
        }
    }
}