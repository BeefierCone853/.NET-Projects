using System.Net;
using System.Net.Http.Json;
using Application.Features.BlogPosts.DTOs;
using Application.Helpers;
using FluentAssertions;
using WebApi.FunctionalTests.Abstractions;

namespace WebApi.FunctionalTests.BlogPosts;

[Collection(nameof(SharedTestCollection))]
public class GetBlogPostListTests(FunctionalTestWebAppFactory factory)
    : BaseFunctionalTest(factory), IAsyncLifetime
{
    [Fact]
    public async Task Should_ReturnOk_WithBlogPostDtos_WhenBlogPostExists()
    {
        // Arrange
        await CreateBlogPostsAsync();

        // Act
        var response = await AuthorizedHttpClient.GetAsync($"{BlogPostEndpoint}?Page=1&PageSize=10");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<PagedList<BlogPostDto>>();
        result?.Items.Count.Should().Be(2);
    }

    [Fact]
    public async Task Should_ReturnOk_WithEmptyList_WhenBlogPostTableIsEmpty()
    {
        // Act
        var response = await AuthorizedHttpClient.GetAsync($"{BlogPostEndpoint}?Page=1&PageSize=10");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<PagedList<BlogPostDto>>();
        result?.Items.Should().BeEmpty();
    }

    [Fact]
    public async Task Should_ReturnOk_WithSortedListByTitle_WhenBlogPostExists()
    {
        // Arrange
        await CreateBlogPostsAsync();

        // Act
        var response = await AuthorizedHttpClient.GetAsync($"{BlogPostEndpoint}?Page=1&PageSize=10&?SortColumn=title");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<PagedList<BlogPostDto>>();
        result?.Items.Count.Should().Be(2);
        result?.Items[0].Title.Should().Be(FirstTitle);
    }

    [Fact]
    public async Task Should_ReturnOk_WithSortedListByTitleAndDescendingOrder_WhenBlogPostExists()
    {
        // Arrange
        await CreateBlogPostsAsync();
        
        // Act
        var response =
            await AuthorizedHttpClient.GetAsync($"{BlogPostEndpoint}?Page=1&PageSize=10&?SortColumn=title&SortOrder=desc");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<PagedList<BlogPostDto>>();
        result?.Items.Count.Should().Be(2);
        result?.Items[0].Title.Should().Be(SecondTitle);
    }

    [Fact]
    public async Task Should_ReturnOk_WithSortedListByDescription_WhenBlogPostExists()
    {
        // Arrange
        await CreateBlogPostsAsync();
        
        // Act
        var response = await AuthorizedHttpClient.GetAsync($"{BlogPostEndpoint}?Page=1&PageSize=10&?SortColumn=description");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<PagedList<BlogPostDto>>();
        result?.Items.Count.Should().Be(2);
        result?.Items[0].Description.Should().Be(FirstDescription);
    }

    [Fact]
    public async Task Should_ReturnOk_WithSortedListByDescriptionAndDescendingOrder_WhenBlogPostExists()
    {
        // Arrange
        await CreateBlogPostsAsync();
        
        // Act
        var response =
            await AuthorizedHttpClient.GetAsync($"{BlogPostEndpoint}?Page=1&PageSize=10&?SortColumn=description&SortOrder=desc");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<PagedList<BlogPostDto>>();
        result?.Items.Count.Should().Be(2);
        result?.Items[0].Description.Should().Be(SecondDescription);
    }

    [Fact]
    public async Task Should_ReturnOk_WithSortedListAndObjectsContainingTheSearchTerm_WhenBlogPostExists()
    {
        // Arrange
        await CreateBlogPostsAsync();
        
        // Act
        var response =
            await AuthorizedHttpClient.GetAsync($"{BlogPostEndpoint}?Page=1&PageSize=10&SearchTerm=abc");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<PagedList<BlogPostDto>>();
        result?.Items.Count.Should().Be(1);
        result?.Items[0].Title.Should().Be(FirstTitle);
        result?.Items[0].Description.Should().Be(FirstDescription);
    }

    [Fact]
    public async Task Should_Return500Error_WhenPageParameterIsZero()
    {
        // Arrange
        await CreateBlogPostsAsync();
        
        // Act
        var response = await AuthorizedHttpClient.GetAsync($"{BlogPostEndpoint}?Page=0&PageSize=10");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }

    public Task InitializeAsync() => ResetApplicationDatabase();

    public Task DisposeAsync() => Task.CompletedTask;
}