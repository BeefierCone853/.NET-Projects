using Application.Features.BlogPosts.DTOs;
using Application.Features.BlogPosts.Queries.GetBlogPostList;
using Domain.Entities;
using Domain.Repositories;
using Domain.Shared;
using FluentAssertions;
using NSubstitute;

namespace Application.UnitTests.Features.BlogPosts;

public class GetBlogPostListQueryTests
{
    private static readonly IReadOnlyList<BlogPost> BlogPostList =
    [
        new BlogPost { Title = "Title1", Description = "Title1", Id = 1 },
        new BlogPost { Title = "Title2", Description = "Title2", Id = 2 },
        new BlogPost { Title = "Title3", Description = "Title3", Id = 3 },
    ];

    private static readonly GetBlogPostListQuery Command = new();
    private readonly GetBlogPostListQueryHandler _handler;
    private readonly IBlogPostRepository _blogPostRepositoryMock;

    public GetBlogPostListQueryTests()
    {
        _blogPostRepositoryMock = Substitute.For<IBlogPostRepository>();
        _handler = new GetBlogPostListQueryHandler(_blogPostRepositoryMock);
    }

    [Fact]
    public async Task Handle_Should_ReturnResult_WithBlogPostDtoList()
    {
        // Arrange
        _blogPostRepositoryMock.GetAll().Returns(BlogPostList);

        // Act
        Result<List<BlogPostDto>> result = await _handler.Handle(Command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.As<Result<List<BlogPostDto>>>();
        result.Value.Count.Should().Be(3);
    }

    [Fact]
    public async Task Handle_Should_CallRepository()
    {
        // Act
        await _handler.Handle(Command, default);

        // Assert
        await _blogPostRepositoryMock.Received(1).GetAll();
    }
}