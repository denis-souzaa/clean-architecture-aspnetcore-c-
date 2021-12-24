using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArch.Application.DTOs;
using CleanArch.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
        {
            var categories = await _categoryService.GetCategories();
            if (categories == null)
            {
                return NoContent();
            }
            return Ok(categories);
        }
        
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoryDTO>> Get(int id)
        {
            var categories = await _categoryService.GetById(id);
            if (categories == null)
            {
                return NotFound("Category not found");
            }
            return Ok(categories);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CategoryDTO categoryDto)
        {
            if(categoryDto == null)
                return BadRequest("Invalid data");

            await _categoryService.Add(categoryDto);
            return Created("category created", categoryDto);
        }

        [HttpPut]
        public async Task<ActionResult> Put(int id, [FromBody] CategoryDTO categoryDto)
        {
            if (id != categoryDto.Id)
                return BadRequest("Id not is valid");

            await _categoryService.Update(categoryDto);
            return Ok(categoryDto);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CategoryDTO>> Delete(int id)
        {
            var category = await _categoryService.GetById(id);

            if (category == null)
            {
                return NotFound("Category not found");
            }

            await _categoryService.Remove(id);
            return Ok(category);
        }
    }
}