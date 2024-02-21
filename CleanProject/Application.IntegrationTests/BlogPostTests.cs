using Application.Exceptions;
using Application.Features.BlogPosts.Commands.CreateBlogPost;
using Application.Features.BlogPosts.Commands.DeleteBlogPost;
using Application.Features.BlogPosts.DTOs;
using Application.Features.BlogPosts.Queries.GetBlogPostById;
using Application.IntegrationTests.Abstractions;
using Domain.Entities;

namespace Application.IntegrationTests;

public class BlogPostTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task GetById_ShouldReturnBlogPost_WhenBlogPostExists()
    {
        // Arrange
        var blogPostDto = new CreateBlogPostDto("Title", "Description");
        var command = new CreateBlogPostCommand(blogPostDto);
        var blogPostId = await Sender.Send(command);
        var query = new GetBlogPostByIdQuery(blogPostId.Value);

        // Act
        var blogPost = await Sender.Send(query);

        // Assert
        Assert.NotNull(blogPost);
    }

    [Fact]
    public async Task Create_ShouldAdd_WhenBlogPostIsValid()
    {
        // Arrange
        var blogPostDto = new CreateBlogPostDto("Title", "Description");
        var command = new CreateBlogPostCommand(blogPostDto);

        // Act
        var blogPostId = await Sender.Send(command);

        // Assert
        var blogPost = DbContext.BlogPosts.FirstOrDefault(blogPost => blogPost.Id == blogPostId.Value);
        Assert.NotNull(blogPost);
    }

    [Fact]
    public async Task Create_ShouldThrowCustomValidationException_WhenTitleIsEmpty()
    {
        // Arrange
        var blogPostDto = new CreateBlogPostDto("", "Description");
        var command = new CreateBlogPostCommand(blogPostDto);

        // Act
        Task Action() => Sender.Send(command);

        // Assert
        await Assert.ThrowsAsync<CustomValidationException>(Action);
    }

    [Fact]
    public async Task Create_ShouldThrowCustomValidationException_WhenDescriptionIsEmpty()
    {
        // Arrange
        var blogPostDto = new CreateBlogPostDto("Title", "");
        var command = new CreateBlogPostCommand(blogPostDto);

        // Act
        Task Action() => Sender.Send(command);

        // Assert
        await Assert.ThrowsAsync<CustomValidationException>(Action);
    }

    [Fact]
    public async Task Create_ShouldThrowCustomValidationException_WhenTitleAndDescriptionAreEmpty()
    {
        // Arrange
        var blogPostDto = new CreateBlogPostDto("", "");
        var command = new CreateBlogPostCommand(blogPostDto);

        // Act
        Task Action() => Sender.Send(command);

        // Assert
        await Assert.ThrowsAsync<CustomValidationException>(Action);
    }

    [Fact]
    public async Task Delete_ShouldDelete_WhenBlogPostExists()
    {
        // Arrange
        const int id = 1;
        var blogPostDto = new CreateBlogPostDto("Title", "Description");
        var createCommand = new CreateBlogPostCommand(blogPostDto);
        await Sender.Send(createCommand);
        var deleteCommand = new DeleteBlogPostCommand(id);
        var existingBlogPost = DbContext.BlogPosts.FirstOrDefault(blogPost => blogPost.Id == id);
        Assert.NotNull(existingBlogPost);

        // Act
        await Sender.Send(deleteCommand);

        // Assert
        var blogPost = DbContext.BlogPosts.FirstOrDefault(blogPost => blogPost.Id == id);
        Assert.Null(blogPost);
    }
}