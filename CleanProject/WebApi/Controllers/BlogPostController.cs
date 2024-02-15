using Application.DTOs.BlogPosts;
using Application.Features.BlogPosts.Commands.CreateBlogPost;
using Application.Features.BlogPosts.Queries.GetBlogPostById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogPostController(ISender sender) : ApiController(sender)
{
    // GET api/<BlogPostController>/5
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetBlogPostById(int id, CancellationToken cancellationToken)
    {
        var query = new GetBlogPostByIdQuery(id);
        var result = await Sender.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    // POST api/<BlogPostController>
    [HttpPost]
    public async Task<IActionResult> CreateBlogPost(
        CreateBlogPostDto createBlogPostDto,
        CancellationToken cancellationToken)
    {
        var command = new CreateBlogPostCommand(createBlogPostDto);
        var result = await Sender.Send(command, cancellationToken);
        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }
}