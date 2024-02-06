using CoreProjectAPI.Models.Domain;
using CoreProjectAPI.Models.DTO.BlogImage;
using CoreProjectAPI.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CoreProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController(IImageRepository imageRepository) : ControllerBase
    {
        // GET: 'http://localhost:5204/api/images'
        [HttpGet]
        public async Task<IActionResult> GetAllImages()
        {
            var images = await imageRepository.GetAllAsync();
            var response = images
                .Select(image => new BlogImageDto(
                    Id: image.Id,
                    Title: image.Title,
                    DateCreated: image.DateCreated,
                    FileExtension: image.FileExtension,
                    FileName: image.FileName,
                    Url: image.Url));
            return Ok(response);
        }

        // POST: 'http://localhost:5204/api/images'
        [HttpPost]
        public async Task<IActionResult> UploadImage(
            [FromForm] IFormFile file,
            [FromForm] string fileName,
            [FromForm] string title)
        {
            ValidateFileUpload(file);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var blogImage = new BlogImage
            {
                FileExtension = Path.GetExtension(file.FileName).ToLower(),
                FileName = fileName,
                Title = title,
                DateCreated = DateTime.Now
            };
            blogImage = await imageRepository.Upload(
                imageFile: file,
                blogImage: blogImage);
            var response = new BlogImageDto(
                Id: blogImage.Id,
                Title: blogImage.Title,
                DateCreated: blogImage.DateCreated,
                FileExtension: blogImage.FileExtension,
                FileName: blogImage.FileName,
                Url: blogImage.Url);
            return Ok(response);
        }

        private void ValidateFileUpload(IFormFile imageFile)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            if (!allowedExtensions.Contains(Path.GetExtension(imageFile.FileName).ToLower()))
            {
                ModelState.AddModelError("file", "Unsupported file format.");
            }

            if (imageFile.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size cannot be more than 10MB");
            }
        }
    }
}