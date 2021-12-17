using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AutoMapper;
using CleanArch.Application.DTOs;
using CleanArch.Application.Interfaces;
using CleanArch.Application.Mappings;
using CleanArch.Application.Services;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace CleanArch.Application.Tests
{
    public class CategoryServiceUnitTest
    {
        private readonly Mock<ICategoryRepository> _repositoryMock;
        private static IMapper _mapper;
        private readonly ICategoryService _categoryService;

        public CategoryServiceUnitTest()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new DomainToDtoMappingProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
            _repositoryMock = new Mock<ICategoryRepository>();
            _categoryService = new CategoryService(_repositoryMock.Object, _mapper);
        }
        
        [Fact(DisplayName = "Get All categories")]
        public async void GetCategories_ShouldBeReturn_IEnumerable_CategoryDTO()
        {
            var categoriesEntity = new List<Category>
            {
                new Category(1, "Category 1"),
                new Category(1, "Category 2"),
                new Category(1, "Category 3")
            };

            _repositoryMock.Setup(c => c.GetCategoriesAsync()).ReturnsAsync(categoriesEntity);
            var categoriesDtos = await _categoryService.GetCategories();
            
            categoriesDtos.Should().NotBeNull();
            categoriesDtos.Should().HaveCount(3);
            _repositoryMock.Verify(c => c.GetCategoriesAsync(), Times.Once());
        }

        [Fact(DisplayName = "Get category by id")]
        public async void GetCategoryById_ShouldBeReturn_OneCategoryDTO()
        {
            var categoryEntity = new Category(1, "Category 1");
            const int categoryId = 1;

            _repositoryMock.Setup(c => c.GetByIdAsync(categoryId)).ReturnsAsync(categoryEntity);
            var categoryDto = await _categoryService.GetById(categoryId);

            categoryDto.Should().NotBeNull();
            categoryDto.Name.Should().Be("Category 1");
            _repositoryMock.Verify(c => c.GetByIdAsync(categoryId),Times.Once());
        }

        [Fact(DisplayName = "Add Category valid")]
        public async void AddCategory_ShouldBeAdd_WhenCategoryIsValid()
        {
            var categoryDto = new CategoryDTO
            {
                Id = 1,
                Name = "Category 1"
            };
            
            var categoryEntity = new Category(1, "Category 1");

            _repositoryMock.Setup(c => c.CreateAsync(categoryEntity)).ReturnsAsync(categoryEntity);
            await _categoryService.Add(categoryDto);
            
            _repositoryMock.Verify(c => c.CreateAsync(categoryEntity),Times.Once());
        }
    }
}