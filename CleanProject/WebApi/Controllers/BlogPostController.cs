using Application.Features.BlogPosts.Commands.CreateBlogPost;
using Application.Features.BlogPosts.Commands.DeleteBlogPost;
using Application.Features.BlogPosts.Commands.UpdateBlogPost;
using Application.Features.BlogPosts.DTOs;
using Application.Features.BlogPosts.Queries.GetBlogPostById;
using Application.Features.BlogPosts.Queries.GetBlogPostList;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Abstractions;

namespace WebApi.Controllers;

/// <summary>
/// Controller for handling BlogPost CRUD operations.
/// </summary>
/// <param name="sender">Sends a request through the mediator pipeline.</param>
[Route("api/[controller]")]
[ApiController]
public class BlogPostController(ISender sender) : ApiController(sender)
{
    /// <summary>
    /// Retrieves all blog posts.
    /// </summary>
    /// <param name="cancellationToken">Signals if a task or operation should be cancelled.</param>
    /// <returns>List of blog posts.</returns>
    // GET api/<BlogPostController>
    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetBlogPostList(CancellationToken cancellationToken)
    {
        var query = new GetBlogPostListQuery();
        var result = await Sender.Send(query, cancellationToken);
        return Ok(result.Value);
    }

    /// <summary>
    /// Gets a blog post record with the specific unique identifier.
    /// </summary>
    /// <param name="id">Unique identifier of a blog post.</param>
    /// <param name="cancellationToken">Signals if a task or operation should be cancelled.</param>
    /// <returns>Blog post record with the specified unique identifier.</returns>
    // GET api/<BlogPostController>/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetBlogPostById(int id, CancellationToken cancellationToken)
    {
        var query = new GetBlogPostByIdQuery(id);
        var result = await Sender.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    /// <summary>
    /// Creates a new blog post.
    /// </summary>
    /// <param name="createBlogPostDto">Request containing information about the blog post.</param>
    /// <param name="cancellationToken">Signals if a task or operation should be cancelled.</param>
    /// <returns>Unique identifier of the newly created blog post or a bad request with information about the error.</returns>
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

    /// <summary>
    /// Updates a blog post with the specified unique identifier. 
    /// </summary>
    /// <param name="updateBlogPostDto">Request containing new information about the blog post.</param>
    /// <param name="id">Unique identifier of a blog post.</param>
    /// <param name="cancellationToken">Signals if a task or operation should be cancelled.</param>
    /// <returns>No content or a not found response with additional information.</returns>
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

    /// <summary>
    /// Deletes a blog post with the specified unique identifier.
    /// </summary>
    /// <param name="id">Unique identifier of a blog post.</param>
    /// <param name="cancellationToken">Signals if a task or operation should be cancelled.</param>
    /// <returns>No content or a not found response with additional information.</returns>
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