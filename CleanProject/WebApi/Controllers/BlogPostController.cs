using Application.Features.BlogPosts.Commands.CreateBlogPost;
using Application.Features.BlogPosts.Commands.DeleteBlogPost;
using Application.Features.BlogPosts.Commands.UpdateBlogPost;
using Application.Features.BlogPosts.DTOs;
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
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetBlogPostById(int id, CancellationToken cancellationToken)
    {
        var query = new GetBlogPostByIdQuery(id);
        var result = await Sender.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    // POST api/<BlogPostController>
    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CreateBlogPost(
        CreateBlogPostDto createBlogPostDto,
        CancellationToken cancellationToken)
    {
        var command = new CreateBlogPostCommand(createBlogPostDto);
        var result = await Sender.Send(command, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    // PUT api/<BlogPostController>/5
    [HttpPut("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> UpdateBlogPost(
        [FromBody] UpdateBlogPostDto updateBlogPostDto,
        [FromRoute] int id,
        CancellationToken cancellationToken)
    {
        var command = new UpdateBlogPostCommand(updateBlogPostDto, id);
        var result = await Sender.Send(command, cancellationToken);
        return result.IsSuccess ? NoContent() : NotFound(result.Error);
    }

    // DELETE api/<BlogPostController>/5
    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteBlogPost(
        int id,
        CancellationToken cancellationToken)
    {
        var command = new DeleteBlogPostCommand(id);
        var result = await Sender.Send(command, cancellationToken);
        return result.IsSuccess ? NoContent() : NotFound(result.Error);
    }
}