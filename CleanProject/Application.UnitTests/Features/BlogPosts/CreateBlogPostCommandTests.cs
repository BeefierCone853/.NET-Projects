using Application.Features.BlogPosts.Commands.CreateBlogPost;
using Application.Features.BlogPosts.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Domain.Shared;
using FluentAssertions;
using NSubstitute;

namespace Application.UnitTests.Features.BlogPosts;

public class CreateBlogPostCommandTests
{
    private static readonly CreateBlogPostDto BlogPostDto = new(
        "This is a title",
        "This is a description");

    private static readonly BlogPost BlogPost = new()
    {
        Title = "This is a title",
        Description = "This is a description",
        Id = 5
    };

    private static readonly CreateBlogPostCommand Command = new(BlogPostDto);
    private readonly CreateBlogPostCommandHandler _handler;
    private readonly IBlogPostRepository _blogPostRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;
    private readonly IMapper _mapperMock;

    public CreateBlogPostCommandTests()
    {
        _blogPostRepositoryMock = Substitute.For<IBlogPostRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        _mapperMock = Substitute.For<IMapper>();
        _handler = new CreateBlogPostCommandHandler(
            _blogPostRepositoryMock,
            _unitOfWorkMock,
            _mapperMock);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess()
    {
        // Arrange
        _mapperMock.Map<BlogPost>(BlogPostDto).Returns(BlogPost);

        // Act
        Result result = await _handler.Handle(Command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_CallUnitOfWork()
    {
        // Arrange
        _mapperMock.Map<BlogPost>(BlogPostDto).Returns(BlogPost);

        // Act
        await _handler.Handle(Command, default);

        // Assert
        await _unitOfWorkMock.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_Should_CallRepository()
    {
        // Arrange
        _mapperMock.Map<BlogPost>(BlogPostDto).Returns(BlogPost);

        // Act
        await _handler.Handle(Command, default);

        // Assert
        _blogPostRepositoryMock.Received(1).Add(Arg.Any<BlogPost>());
    }

    [Fact]
    public async Task Handle_Should_CallAutoMapper()
    {
        // Arrange
        _mapperMock.Map<BlogPost>(BlogPostDto).Returns(BlogPost);

        // Act
        await _handler.Handle(Command, default);

        // Assert
        _mapperMock.Received(1).Map<BlogPost>(BlogPostDto);
    }
    
    [Fact]
    public async Task Handle_Should_ReturnId_WhenUserIsCreated()
    {
        // Arrange
        _mapperMock.Map<BlogPost>(BlogPostDto).Returns(BlogPost);
        _blogPostRepositoryMock.Add(BlogPost);

        // Act
        var result = await _handler.Handle(Command, default);

        // Assert
        result.Value.Should().Be(5);
    }
}