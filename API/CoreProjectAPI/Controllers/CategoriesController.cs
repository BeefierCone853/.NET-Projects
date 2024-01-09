using CoreProjectAPI.Data;
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
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequestDto request)
        {
            var category = new Category
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle,
            };
            await categoryRepository.CreateAsync(category);
            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle,
            };
            return Ok(response);
        }
    }
}
