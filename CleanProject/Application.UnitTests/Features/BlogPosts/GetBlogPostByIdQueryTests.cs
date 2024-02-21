using Application.Features.BlogPosts.DTOs;
using Application.Features.BlogPosts.Queries.GetBlogPostById;
using AutoMapper;
using Domain.Entities;
using Domain.Features.BlogPosts;
using Domain.Repositories;
using Domain.Shared;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace Application.UnitTests.Features.BlogPosts;

public class GetBlogPostByIdQueryTests
{
    private const string Title = "This is the title";
    private const string Description = "This is the description";
    private static readonly GetBlogPostByIdQuery Command = new(1);
    private static readonly BlogPostDto BlogPostDto = new(Title, Description, Command.Id);
    private readonly BlogPost _blogPost = new();
    private readonly GetBlogPostByIdQueryHandler _handler;
    private readonly IBlogPostRepository _blogPostRepositoryMock;
    private readonly IMapper _mapperMock;

    public GetBlogPostByIdQueryTests()
    {
        _blogPost.Title = Title;
        _blogPost.Description = Description;
        _blogPost.Id = Command.Id;
        _blogPostRepositoryMock = Substitute.For<IBlogPostRepository>();
        _mapperMock = Substitute.For<IMapper>();
        _handler = new GetBlogPostByIdQueryHandler(
            _blogPostRepositoryMock,
            _mapperMock);
    }

    [Fact]
    public async Task Handle_Should_ReturnResult_WithBlogPostDto()
    {
        // Arrange
        _blogPostRepositoryMock.GetById(Command.Id).Returns(_blogPost);
        _mapperMock.Map<BlogPostDto>(_blogPost).Returns(BlogPostDto);

        // Act
        Result<BlogPostDto> result = await _handler.Handle(Command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.As<Result<BlogPostDto>>();
        result.Value.Description.Should().Be(Description);
        result.Value.Title.Should().Be(Title);
        result.Value.Id.Should().Be(Command.Id);
    }

    [Fact]
    public async Task Handle_Should_ReturnError_WhenBlogPostIsNull()
    {
        // Arrange
        _blogPostRepositoryMock.GetById(Command.Id).ReturnsNull();

        // Act
        Result result = await _handler.Handle(Command, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(BlogPostsErrors.NotFound(Command.Id));
    }

    [Fact]
    public async Task Handle_Should_CallRepository()
    {
        // Act
        await _handler.Handle(Command, default);

        // Assert
        await _blogPostRepositoryMock.Received(1).GetById(Arg.Is(Command.Id));
    }

    [Fact]
    public async Task Handle_Should_CallAutoMapper()
    {
        // Arrange
        _blogPostRepositoryMock.GetById(Command.Id).Returns(_blogPost);

        // Act
        await _handler.Handle(Command, default);

        // Assert
        _mapperMock.Received(1).Map<BlogPostDto>(_blogPost);
    }
}