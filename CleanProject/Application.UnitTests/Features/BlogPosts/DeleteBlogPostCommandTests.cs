using Application.Features.BlogPosts.Commands.DeleteBlogPost;
using Domain.Entities;
using Domain.Features.BlogPosts;
using Domain.Repositories;
using Domain.Shared;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace Application.UnitTests.Features.BlogPosts;

public class DeleteBlogPostCommandTests
{
    private const int Id = 1;

    private static readonly BlogPost BlogPost = new()
    {
        Title = "This is a title",
        Description = "This is a description",
        Id = Id
    };

    private static readonly DeleteBlogPostCommand Command = new(Id);
    private readonly DeleteBlogPostCommandHandler _handler;
    private readonly IBlogPostRepository _blogPostRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;

    public DeleteBlogPostCommandTests()
    {
        _blogPostRepositoryMock = Substitute.For<IBlogPostRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        _handler = new DeleteBlogPostCommandHandler(
            _blogPostRepositoryMock,
            _unitOfWorkMock);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenEntityExists()
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
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(BlogPostsErrors.NotFound(Id));
    }

    [Fact]
    public async Task Handle_Should_CallRepositoryGetById()
    {
        // Act
        await _handler.Handle(Command, default);

        // Assert
        await _blogPostRepositoryMock.Received(1).GetById(Arg.Is(Command.BlogPostId));
    }

    [Fact]
    public async Task Handle_Should_CallRepositoryGetByIdAndDelete()
    {
        // Arrange
        _blogPostRepositoryMock.GetById(Id).Returns(BlogPost);

        // Act
        await _handler.Handle(Command, default);

        // Assert
        await _blogPostRepositoryMock.Received(1).GetById(Arg.Is(Command.BlogPostId));
        _blogPostRepositoryMock.Received(1).Delete(Arg.Is(BlogPost));
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