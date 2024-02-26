using Application.Abstractions.Data;
using Application.Features.BlogPosts.Commands.UpdateBlogPost;
using Application.Features.BlogPosts.DTOs;
using Domain.Entities;
using Domain.Features.BlogPosts;
using Domain.Repositories;
using Domain.Shared;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace Application.UnitTests.Features.BlogPosts;

public class UpdateBlogPostCommandTest
{
    private const int Id = 1;
    private const string Title = "This is a title";
    private const string Description = "This is a description";

    private static readonly UpdateBlogPostDto BlogPostDto = new(Title, Description);

    private static readonly BlogPost BlogPost = new()
    {
        Title = Title,
        Description = Description,
        Id = Id
    };

    private static readonly UpdateBlogPostCommand Command = new(BlogPostDto, Id);
    private readonly UpdateBlogPostCommandHandler _handler;
    private readonly IBlogPostRepository _blogPostRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;

    public UpdateBlogPostCommandTest()
    {
        _blogPostRepositoryMock = Substitute.For<IBlogPostRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        _handler = new UpdateBlogPostCommandHandler(
            _blogPostRepositoryMock,
            _unitOfWorkMock);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess()
    {
        // Arrange
        _blogPostRepositoryMock.GetById(Id).Returns(BlogPost);

        // Act
        Result result = await _handler.Handle(Command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_ReturnError_WhenBlogPostIsNull()
    {
        // Arrange
        _blogPostRepositoryMock.GetById(Id).ReturnsNull();

        // Act
        Result result = await _handler.Handle(Command, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(BlogPostsErrors.NotFound(Id));
    }

    [Fact]
    public async Task Handle_Should_CallRepositoryGetById()
    {
        // Act
        await _handler.Handle(Command, default);

        // Assert
        await _blogPostRepositoryMock.Received(1).GetById(Arg.Is(Command.Id));
    }

    [Fact]
    public async Task Handle_Should_CallRepositoryGetByIdAndUpdate()
    {
        // Arrange
        _blogPostRepositoryMock.GetById(Id).Returns(BlogPost);

        // Act
        await _handler.Handle(Command, default);

        // Assert
        await _blogPostRepositoryMock.Received(1).GetById(Arg.Is(Command.Id));
        _blogPostRepositoryMock.Received(1).Update(Arg.Is(BlogPost));
    }

    [Fact]
    public async Task Handle_Should_CallUnitOfWork()
    {
        // Arrange
        _blogPostRepositoryMock.GetById(Id).Returns(BlogPost);

        // Act
        await _handler.Handle(Command, default);

        // Assert
        await _unitOfWorkMock.Received().SaveChangesAsync(Arg.Any<CancellationToken>());
    }
}