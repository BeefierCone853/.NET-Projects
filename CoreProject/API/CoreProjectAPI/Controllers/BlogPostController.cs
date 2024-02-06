using CoreProjectAPI.Models.Domain;
using CoreProjectAPI.Models.DTO;
using CoreProjectAPI.Models.DTO.BlogPost;
using CoreProjectAPI.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Writer")]
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

        // GET: 'http://localhost:5204/api/blogpost/{id}'
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetBlogPostById([FromRoute] Guid id)
        {
            var blogPost = await blogPostRepository.GetByIdAsync(id);
            if (blogPost is null)
            {
                return NotFound();
            }

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

        // GET: 'http://localhost:5204/api/blogpost/{urlHandle}'
        [HttpGet]
        [Route("{urlHandle}")]
        public async Task<IActionResult> GetBlogPostByUrlHandle([FromRoute] string urlHandle)
        {
            var blogPost = await blogPostRepository.GetByUrlHandleAsync(urlHandle);
            if (blogPost is null)
            {
                return NotFound();
            }

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

        // PUT: 'http://localhost:5204/api/blogpost/{id}'
        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> EditBlogPostById(
            [FromRoute] Guid id,
            UpdateBlogPostRequestDto request)
        {
            var blogPost = new BlogPost
            {
                Id = id,
                Categories = new List<Category>(),
                UrlHandle = request.UrlHandle,
                PublishedDate = request.PublishedDate,
                ShortDescription = request.ShortDescription,
                Author = request.Author,
                Title = request.Title,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                IsVisible = request.IsVisible
            };
            foreach (var categoryGuid in request.Categories)
            {
                var existingCategory = await categoryRepository.GetByIdAsync(categoryGuid);
                if (existingCategory is not null)
                {
                    blogPost.Categories.Add(existingCategory);
                }
            }

            var updatedBlogPost = await blogPostRepository.EditAsync(blogPost);
            if (updatedBlogPost is null)
            {
                return NotFound();
            }

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

        // DELETE: 'http://localhost:5204/api/blogpost/{id}'
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteBlogPostById([FromRoute] Guid id)
        {
            var deletedBlogPost = await blogPostRepository.DeleteAsync(id);
            if (deletedBlogPost is null)
            {
                return NotFound();
            }

            var response = new BlogPostDto(
                Id: deletedBlogPost.Id,
                Author: deletedBlogPost.Author,
                Title: deletedBlogPost.Title,
                Content: deletedBlogPost.Content,
                FeaturedImageUrl: deletedBlogPost.FeaturedImageUrl,
                IsVisible: deletedBlogPost.IsVisible,
                UrlHandle: deletedBlogPost.UrlHandle,
                PublishedDate: deletedBlogPost.PublishedDate,
                ShortDescription: deletedBlogPost.ShortDescription,
                Categories: deletedBlogPost.Categories.Select(
                    category => new CategoryDto(
                        Id: category.Id,
                        Name: category.Name,
                        UrlHandle: category.UrlHandle)
                ).ToList()
            );
            return Ok(response);
        }
    }
}