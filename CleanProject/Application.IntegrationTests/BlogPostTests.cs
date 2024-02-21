using Application.Exceptions;
using Application.Features.BlogPosts.Commands.CreateBlogPost;
using Application.Features.BlogPosts.Commands.DeleteBlogPost;
using Application.Features.BlogPosts.Commands.UpdateBlogPost;
using Application.Features.BlogPosts.DTOs;
using Application.Features.BlogPosts.Queries.GetBlogPostById;
using Application.Features.BlogPosts.Queries.GetBlogPostList;
using Application.IntegrationTests.Abstractions;
using Domain.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Application.IntegrationTests;

public class BlogPostTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task GetList_ShouldReturnNonEmptyBlogPostDtoList_WhenBlogPostExists()
    {
        // Arrange
        var blogPostDto = new CreateBlogPostDto("Title", "Description");
        var command = new CreateBlogPostCommand(blogPostDto);
        await Sender.Send(command);
        blogPostDto = new CreateBlogPostDto("Title2", "Description2");
        command = new CreateBlogPostCommand(blogPostDto);
        await Sender.Send(command);
        var getListCommand = new GetBlogPostListQuery();

        // Act
        await Sender.Send(getListCommand);

        // Assert
        var blogPosts = await DbContext.Set<BlogPost>().ToListAsync();
        Assert.NotEmpty(blogPosts);
        blogPosts.Count.Should().Be(2);
    }
    
    [Fact]
    public async Task GetList_ShouldReturnEmptyBlogPostDtoList_WhenBlogPostTableIsEmpty()
    {
        // Arrange
        var getListCommand = new GetBlogPostListQuery();

        // Act
        await Sender.Send(getListCommand);

        // Assert
        var blogPosts = await DbContext.Set<BlogPost>().ToListAsync();
        Assert.Empty(blogPosts);
        blogPosts.Count.Should().Be(0);
    }

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
    public async Task Update_ShouldUpdate_WhenBlogPostExists()
    {
        // Arrange
        const int id = 1;
        var blogPostDto = new CreateBlogPostDto("Title", "Description");
        var createCommand = new CreateBlogPostCommand(blogPostDto);
        await Sender.Send(createCommand);
        var updateBlogPostDto = new UpdateBlogPostDto("New title", "New description");
        var updateCommand = new UpdateBlogPostCommand(updateBlogPostDto, id);
        var existingBlogPost = DbContext.BlogPosts.FirstOrDefault(blogPost => blogPost.Id == id);
        Assert.NotNull(existingBlogPost);

        // Act
        await Sender.Send(updateCommand);

        // Assert
        var blogPost = DbContext.BlogPosts.FirstOrDefault(blogPost => blogPost.Id == id);
        Assert.NotNull(blogPost);
        blogPost.Title.Should().Be("New title");
        blogPost.Description.Should().Be("New description");
    }

    [Fact]
    public async Task Update_ShouldThrowCustomValidationException_WhenTitleIsEmpty()
    {
        // Arrange
        var blogPostDto = new UpdateBlogPostDto("", "Description");
        var command = new UpdateBlogPostCommand(blogPostDto, 1);

        // Act
        Task Action() => Sender.Send(command);

        // Assert
        await Assert.ThrowsAsync<CustomValidationException>(Action);
    }

    [Fact]
    public async Task Update_ShouldThrowCustomValidationException_WhenDescriptionIsEmpty()
    {
        // Arrange
        var blogPostDto = new UpdateBlogPostDto("Title", "");
        var command = new UpdateBlogPostCommand(blogPostDto, 1);

        // Act
        Task Action() => Sender.Send(command);

        // Assert
        await Assert.ThrowsAsync<CustomValidationException>(Action);
    }

    [Fact]
    public async Task Update_ShouldThrowCustomValidationException_WhenTitleAndDescriptionAreEmpty()
    {
        // Arrange
        var blogPostDto = new UpdateBlogPostDto("", "");
        var command = new UpdateBlogPostCommand(blogPostDto, 1);

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