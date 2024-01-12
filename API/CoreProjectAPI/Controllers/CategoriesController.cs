using CoreProjectAPI.Models.Domain;
using CoreProjectAPI.Models.DTO;
using CoreProjectAPI.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CoreProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(ICategoryRepository categoryRepository) : ControllerBase
    {
        // POST: 'http://localhost:5204/api/categories'
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequestDto request)
        {
            var category = new Category
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle,
            };
            await categoryRepository.CreateAsync(category);
            var response = new CategoryDto(
                Id: category.Id,
                Name: category.Name,
                UrlHandle: category.UrlHandle);
            return Ok(response);
        }

        // GET: 'http://localhost:5204/api/categories'
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await categoryRepository.GetAllAsync();
            var response = categories
                .Select(category => new CategoryDto(
                    category.Id,
                    category.Name,
                    category.UrlHandle)).ToList();
            return Ok(response);
        }

        // GET: http://localhost:5204/api/categories/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid id)
        {
            var category = await categoryRepository.GetByIdAsync(id);
            if (category is null)
            {
                return NotFound();
            }

            var response = new CategoryDto(
                Id: category.Id,
                Name: category.Name,
                UrlHandle: category.UrlHandle);
            return Ok(response);
        }

        // PUT: http://localhost:5204/api/categories/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> EditCategoryById(
            [FromRoute] Guid id,
            UpdateCategoryRequestDto request)
        {
            var category = new Category
            {
                Id = id,
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };
            category = await categoryRepository.EditAsync(category);
            if (category is null)
            {
                return NotFound();
            }

            var response = new CategoryDto(
                Id: category.Id,
                Name: category.Name,
                UrlHandle: category.UrlHandle
            );
            return Ok(response);
        }
        
        // DELETE: http://localhost:5204/api/categories/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteCategoryById([FromRoute] Guid id)
        {
            var category = await categoryRepository.DeleteAsync(id);
            if (category is null)
            {
                return NotFound();
            }

            var response = new CategoryDto(
                Id: category.Id,
                Name: category.Name,
                UrlHandle: category.UrlHandle);
            return Ok(response);
        }
    }
}