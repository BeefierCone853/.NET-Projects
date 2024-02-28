using Application.Features.BlogPosts;
using Application.Features.BlogPosts.Commands.CreateBlogPost;
using Application.Features.BlogPosts.Commands.DeleteBlogPost;
using Application.Features.BlogPosts.Commands.UpdateBlogPost;
using Application.Features.BlogPosts.DTOs;
using Application.Features.BlogPosts.Queries.GetBlogPostById;
using Application.Features.BlogPosts.Queries.GetBlogPostList;
using Application.Helpers;
using Application.IntegrationTests.Abstractions;
using Domain.Entities;
using Domain.Features.BlogPosts;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ValidationError = Domain.Shared.ValidationError;

namespace Application.IntegrationTests;

public class BlogPostTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private static readonly SearchQuery SearchQuery = new SearchQuery(
        null,
        null,
        null,
        1,
        10);

    [Fact]
    public async Task GetList_ShouldReturnNonEmptyBlogPostDtoList_WhenBlogPostExists()
    {
        // Arrange
        await CreateBlogPostsAsync();
        var getListCommand = new GetBlogPostListQuery(SearchQuery);

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
        var getListCommand = new GetBlogPostListQuery(SearchQuery);

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
        const string title = "customTitle";
        const string description = "customDescription";
        var createResult = await CreateBlogPostAsync(title, description);
        var query = new GetBlogPostByIdQuery(createResult.Value);

        // Act
        var result = await Sender.Send(query);

        // Assert
        Assert.True(result.IsSuccess);
        result.Value.Title.Should().Be(title);
        result.Value.Description.Should().Be(description);
    }

    [Fact]
    public async Task GetById_ShouldReturnNotFound_WhenBlogPostIsNotFound()
    {
        // Arrange
        const int id = 1;
        var query = new GetBlogPostByIdQuery(id);

        // Act
        var result = await Sender.Send(query);

        // Assert
        Assert.True(result.IsFailure);
        result.Error.Code.Should().Be(BlogPostsErrors.NotFound(id).Code);
        result.Error.Description.Should().Be(BlogPostsErrors.NotFound(id).Description);
    }

    [Fact]
    public async Task Create_ShouldAdd_WhenBlogPostIsValid()
    {
        // Arrange
        var blogPostDto = new CreateBlogPostDto("Title2", "Description2");
        var command = new CreateBlogPostCommand(blogPostDto);

        // Act
        var result = await Sender.Send(command);

        // Assert
        var blogPost = DbContext.BlogPosts.FirstOrDefault(blogPost => blogPost.Id == result.Value);
        Assert.NotNull(blogPost);
    }

    [Fact]
    public async Task Create_ShouldReturnError_WithMissingTitleErrorCode_WhenTitleIsEmpty()
    {
        // Arrange
        var blogPostDto = new CreateBlogPostDto("", "Description");
        var command = new CreateBlogPostCommand(blogPostDto);

        // Act
        var result = await Sender.Send(command);

        // Assert
        var error = (ValidationError)result.Error;
        error.Errors.Select(e => e.Code).Should()
            .Contain(BlogPostErrorCodes.SharedCreateUpdateBlogPost.MissingTitle);
    }

    [Fact]
    public async Task Create_ShouldReturnError_WithMissingDescriptionErrorCode_WhenDescriptionIsEmpty()
    {
        // Arrange
        var blogPostDto = new CreateBlogPostDto("Title", "");
        var command = new CreateBlogPostCommand(blogPostDto);

        // Act
        var result = await Sender.Send(command);

        // Assert
        var error = (ValidationError)result.Error;
        error.Errors.Select(e => e.Code).Should()
            .Contain(BlogPostErrorCodes.SharedCreateUpdateBlogPost.MissingDescription);
    }

    [Fact]
    public async Task
        Create_ShouldReturnError_WithMissingTitleAndDescriptionErrorCodes_WhenTitleAndDescriptionAreEmpty()
    {
        // Arrange
        var blogPostDto = new CreateBlogPostDto("", "");
        var command = new CreateBlogPostCommand(blogPostDto);

        // Act
        var result = await Sender.Send(command);

        // Assert
        var error = (ValidationError)result.Error;
        error.Errors.Select(e => e.Code).Should().Contain([
            BlogPostErrorCodes.SharedCreateUpdateBlogPost.MissingTitle,
            BlogPostErrorCodes.SharedCreateUpdateBlogPost.MissingDescription
        ]);
    }

    [Fact]
    public async Task Update_ShouldUpdate_WhenBlogPostExists()
    {
        // Arrange
        var createResult = await CreateBlogPostAsync();
        var updateBlogPostDto = new UpdateBlogPostDto("New title", "New description");
        var updateCommand = new UpdateBlogPostCommand(updateBlogPostDto, createResult.Value);
        var existingBlogPost = DbContext.BlogPosts.FirstOrDefault(blogPost => blogPost.Id == createResult.Value);
        Assert.NotNull(existingBlogPost);

        // Act
        await Sender.Send(updateCommand);

        // Assert
        var blogPost = DbContext.BlogPosts.FirstOrDefault(blogPost => blogPost.Id == createResult.Value);
        Assert.NotNull(blogPost);
        blogPost.Title.Should().Be("New title");
        blogPost.Description.Should().Be("New description");
    }

    [Fact]
    public async Task Update_ShouldReturnError_WithMissingTitleErrorCode_WhenTitleIsEmpty()
    {
        // Arrange
        var blogPostDto = new UpdateBlogPostDto("", "Description");
        var command = new UpdateBlogPostCommand(blogPostDto, 1);

        // Act
        var result = await Sender.Send(command);

        // Assert
        var error = (ValidationError)result.Error;
        error.Errors.Select(e => e.Code).Should()
            .Contain(BlogPostErrorCodes.SharedCreateUpdateBlogPost.MissingTitle);
    }

    [Fact]
    public async Task Update_ShouldReturnError_WithMissingDescriptionErrorCode_WhenDescriptionIsEmpty()
    {
        // Arrange
        var blogPostDto = new UpdateBlogPostDto("Title", "");
        var command = new UpdateBlogPostCommand(blogPostDto, 1);

        // Act
        var result = await Sender.Send(command);

        // Assert
        var error = (ValidationError)result.Error;
        error.Errors.Select(e => e.Code).Should()
            .Contain(BlogPostErrorCodes.SharedCreateUpdateBlogPost.MissingDescription);
    }

    [Fact]
    public async Task
        Update_ShouldReturnError_WithMissingTitleAndDescriptionErrorCodes_WhenTitleAndDescriptionAreEmpty()
    {
        // Arrange
        var blogPostDto = new UpdateBlogPostDto("", "");
        var command = new UpdateBlogPostCommand(blogPostDto, 1);

        // Act
        var result = await Sender.Send(command);

        // Assert
        var error = (ValidationError)result.Error;
        error.Errors.Select(e => e.Code).Should().Contain([
            BlogPostErrorCodes.SharedCreateUpdateBlogPost.MissingTitle,
            BlogPostErrorCodes.SharedCreateUpdateBlogPost.MissingDescription
        ]);
    }

    [Fact]
    public async Task Delete_ShouldDelete_WhenBlogPostExists()
    {
        // Arrange
        var createResult = await CreateBlogPostAsync();
        var deleteCommand = new DeleteBlogPostCommand(createResult.Value);
        var existingBlogPost = DbContext.BlogPosts.FirstOrDefault(blogPost => blogPost.Id == createResult.Value);
        Assert.NotNull(existingBlogPost);

        // Act
        await Sender.Send(deleteCommand);

        // Assert
        var blogPost = DbContext.BlogPosts.FirstOrDefault(blogPost => blogPost.Id == createResult.Value);
        Assert.Null(blogPost);
    }
}