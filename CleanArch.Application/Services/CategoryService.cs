using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CleanArch.Application.DTOs;
using CleanArch.Application.Interfaces;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces;

namespace CleanArch.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        
        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CategoryDTO>> GetCategories()
        {
            return _mapper.Map<IEnumerable<CategoryDTO>>(await _repository.GetCategoriesAsync());
        }

        public async Task<CategoryDTO> GetById(int? id)
        {
            if (!id.HasValue) return null;
            
            return _mapper.Map<CategoryDTO>(await _repository.GetByIdAsync(id));
        }

        public async Task Add(CategoryDTO categoryDto)
        {
            var categoryEntity = _mapper.Map<Category>(categoryDto);
            await _repository.CreateAsync(categoryEntity);
        }

        public async Task Update(CategoryDTO categoryDto)
        {
            var categoryEntity = _mapper.Map<Category>(categoryDto);
            await _repository.UpdateAsync(categoryEntity);
        }

        public async Task Remove(int? id)
        {
            if (!id.HasValue) return;
            var categoryEntity = await _repository.GetByIdAsync(id);
            await _repository.RemoveAsync(categoryEntity);
        }
    }
}