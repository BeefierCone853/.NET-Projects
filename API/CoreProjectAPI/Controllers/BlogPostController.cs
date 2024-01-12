using CoreProjectAPI.Models.Domain;
using CoreProjectAPI.Models.DTO;
using CoreProjectAPI.Models.DTO.BlogPost;
using CoreProjectAPI.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CoreProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController(
        IBlogPostRepository blogPostRepository,
        ICategoryRepository categoryRepository) : ControllerBase
    {
        // POST: 'http://localhost:5204/api/blogpost'
        [HttpPost]
        public async Task<IActionResult> CreateBlogPost([FromBody] CreateBlogPostRequestDto request)
        {
            var blogPost = new BlogPost
            {
                Author = request.Author,
                Title = request.Title,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                IsVisible = request.IsVisible,
                UrlHandle = request.UrlHandle,
                PublishedDate = request.PublishedDate,
                ShortDescription = request.ShortDescription,
                Categories = new List<Category>()
            };
            foreach (var categoryGuid in request.Categories)
            {
                var existingCategory = await categoryRepository.GetByIdAsync(categoryGuid);
                if (existingCategory is not null)
                {
                    blogPost.Categories.Add(existingCategory);
                }
            }

            blogPost = await blogPostRepository.CreateAsync(blogPost);
            var response = new BlogPostDto(
                Id: blogPost.Id,
                Author: blogPost.Author,
                Title: blogPost.Title,
                Content: blogPost.Content,
                FeaturedImageUrl: blogPost.FeaturedImageUrl,
                IsVisible: blogPost.IsVisible,
                UrlHandle: blogPost.UrlHandle,
                PublishedDate: blogPost.PublishedDate,
                ShortDescription: blogPost.ShortDescription,
                Categories: blogPost.Categories.Select(
                    category => new CategoryDto(
                        Id: category.Id,
                        Name: category.Name,
                        UrlHandle: category.UrlHandle)
                ).ToList()
            );
            return Ok(response);
        }

        // GET: 'http://localhost:5204/api/blogpost'
        [HttpGet]
        public async Task<IActionResult> GetAllBlogPosts()
        {
            var blogPosts = await blogPostRepository.GetAllAsync();
            var response = blogPosts.Select(blogPost => new BlogPostDto(
                Id: blogPost.Id,
                Author: blogPost.Author,
                Title: blogPost.Title,
                Content: blogPost.Content,
                FeaturedImageUrl: blogPost.FeaturedImageUrl,
                IsVisible: blogPost.IsVisible,
                UrlHandle: blogPost.UrlHandle,
                PublishedDate: blogPost.PublishedDate,
                ShortDescription: blogPost.ShortDescription,
                Categories: blogPost.Categories.Select(
                    category => new CategoryDto(
                        Id: category.Id,
                        Name: category.Name,
                        UrlHandle: category.UrlHandle)
                ).ToList()));
            return Ok(response);
        }
    }
}