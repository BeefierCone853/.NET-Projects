using Application.Features.BlogPosts.Commands.CreateBlogPost;
using Application.Features.BlogPosts.Commands.DeleteBlogPost;
using Application.Features.BlogPosts.Commands.UpdateBlogPost;
using Application.Features.BlogPosts.DTOs;
using Application.Features.BlogPosts.Queries.GetBlogPostById;
using Application.Features.BlogPosts.Queries.GetBlogPostList;
using Application.Helpers;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;
using WebApi.Infrastructure;

namespace WebApi.Endpoints;

/// <summary>
/// Endpoint for handling BlogPost CRUD operations.
/// </summary>
public class BlogPostEndpoints : ICarterModule
{
    /// <summary>
    /// Groups and adds routes.
    /// </summary>
    /// <param name="app">Route builder.</param>
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("blogposts");
        group.MapPost("", CreateBlogPost);
        group.MapGet("", GetBlogPostList);
        group.MapGet("{id:int}", GetBlogPostById);
        group.MapPut("{id:int}", UpdateBlogPost);
        group.MapDelete("{id:int}", DeleteBlogPost);
    }

    /// <summary>
    /// Retrieves all blog posts.
    /// </summary>
    /// <param name="searchQuery">Query parameters.</param>
    /// <param name="sender">Sends a request through the mediator pipeline.</param>
    /// <param name="cancellationToken">Signals if a task or operation should be cancelled.</param>
    /// <returns>List of blog posts.</returns>
    public static async Task<IResult> GetBlogPostList(
        [AsParameters] SearchQuery searchQuery,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var query = new GetBlogPostListQuery(searchQuery);
        var result = await sender.Send(query, cancellationToken);
        return result.Match(Results.Ok, CustomResults.Problem);
    }

    /// <summary>
    /// Gets a blog post record with the specific unique identifier.
    /// </summary>
    /// <param name="id">Unique identifier of a blog post.</param>
    /// <param name="sender">Sends a request through the mediator pipeline.</param>
    /// <param name="cancellationToken">Signals if a task or operation should be cancelled.</param>
    /// <returns>Blog post record with the specified unique identifier.</returns>
    public static async Task<IResult> GetBlogPostById(
        int id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var query = new GetBlogPostByIdQuery(id);
        var result = await sender.Send(query, cancellationToken);
        return result.Match(Results.Ok, CustomResults.Problem);
    }

    /// <summary>
    /// Creates a new blog post.
    /// </summary>
    /// <param name="createBlogPostDto">Request containing information about the blog post.</param>
    /// <param name="sender">Sends a request through the mediator pipeline.</param>
    /// <param name="cancellationToken">Signals if a task or operation should be cancelled.</param>
    /// <returns>Unique identifier of the newly created blog post or a bad request with information about the error.</returns>
    public static async Task<IResult> CreateBlogPost(
        CreateBlogPostDto createBlogPostDto,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var command = new CreateBlogPostCommand(createBlogPostDto);
        var result = await sender.Send(command, cancellationToken);
        return result.Match(Results.Ok, CustomResults.Problem);
    }

    /// <summary>
    /// Updates a blog post with the specified unique identifier. 
    /// </summary>
    /// <param name="updateBlogPostDto">Request containing new information about the blog post.</param>
    /// <param name="id">Unique identifier of a blog post.</param>
    /// <param name="sender">Sends a request through the mediator pipeline.</param>
    /// <param name="cancellationToken">Signals if a task or operation should be cancelled.</param>
    /// <returns>No content or a not found response with additional information.</returns>
    public static async Task<IResult> UpdateBlogPost(
        [FromBody] UpdateBlogPostDto updateBlogPostDto,
        [FromRoute] int id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var command = new UpdateBlogPostCommand(updateBlogPostDto, id);
        var result = await sender.Send(command, cancellationToken);
        return result.Match(Results.NoContent, CustomResults.Problem);
    }

    /// <summary>
    /// Deletes a blog post with the specified unique identifier.
    /// </summary>
    /// <param name="id">Unique identifier of a blog post.</param>
    /// <param name="sender">Sends a request through the mediator pipeline.</param>
    /// <param name="cancellationToken">Signals if a task or operation should be cancelled.</param>
    /// <returns>No content or a not found response with additional information.</returns>
    public static async Task<IResult> DeleteBlogPost(
        int id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var command = new DeleteBlogPostCommand(id);
        var result = await sender.Send(command, cancellationToken);
        return result.Match(Results.NoContent, CustomResults.Problem);
    }
}