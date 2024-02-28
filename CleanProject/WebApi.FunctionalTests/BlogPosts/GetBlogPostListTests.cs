using System.Net;
using System.Net.Http.Json;
using Application.Features.BlogPosts.DTOs;
using Application.Helpers;
using FluentAssertions;
using WebApi.FunctionalTests.Abstractions;

namespace WebApi.FunctionalTests.BlogPosts;

public class GetBlogPostListTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
{
    [Fact]
    public async Task Should_ReturnOk_WithBlogPostDtos_WhenBlogPostExists()
    {
        // Act
        var response = await HttpClient.GetAsync($"{BlogPostEndpoint}?Page=1&PageSize=10");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<PagedList<BlogPostDto>>();
        result?.Items.Count.Should().Be(2);
    }

    [Fact]
    public async Task Should_ReturnOk_WithSortedListByTitle_WhenBlogPostExists()
    {
        // Act
        var response = await HttpClient.GetAsync($"{BlogPostEndpoint}?Page=1&PageSize=10&?SortColumn=title");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<PagedList<BlogPostDto>>();
        result?.Items.Count.Should().Be(2);
        result?.Items[0].Title.Should().Be(FirstTitle);
    }

    [Fact]
    public async Task Should_ReturnOk_WithSortedListByTitleAndDescendingOrder_WhenBlogPostExists()
    {
        // Act
        var response =
            await HttpClient.GetAsync($"{BlogPostEndpoint}?Page=1&PageSize=10&?SortColumn=title&SortOrder=desc");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<PagedList<BlogPostDto>>();
        result?.Items.Count.Should().Be(2);
        result?.Items[0].Title.Should().Be(SecondTitle);
    }

    [Fact]
    public async Task Should_ReturnOk_WithSortedListByDescription_WhenBlogPostExists()
    {
        // Act
        var response = await HttpClient.GetAsync($"{BlogPostEndpoint}?Page=1&PageSize=10&?SortColumn=description");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<PagedList<BlogPostDto>>();
        result?.Items.Count.Should().Be(2);
        result?.Items[0].Description.Should().Be(FirstDescription);
    }

    [Fact]
    public async Task Should_ReturnOk_WithSortedListByDescriptionAndDescendingOrder_WhenBlogPostExists()
    {
        // Act
        var response =
            await HttpClient.GetAsync($"{BlogPostEndpoint}?Page=1&PageSize=10&?SortColumn=description&SortOrder=desc");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<PagedList<BlogPostDto>>();
        result?.Items.Count.Should().Be(2);
        result?.Items[0].Description.Should().Be(SecondDescription);
    }

    [Fact]
    public async Task Should_ReturnOk_WithSortedListAndObjectsContainingTheSearchTerm_WhenBlogPostExists()
    {
        // Act
        var response =
            await HttpClient.GetAsync($"{BlogPostEndpoint}?Page=1&PageSize=10&SearchTerm=abc");

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
        // Act
        var response = await HttpClient.GetAsync($"{BlogPostEndpoint}?Page=0&PageSize=10");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }
}

public class GetBlogPostListIsolatedTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
{
    [Fact]
    public async Task Should_ReturnOk_WithEmptyList_WhenBlogPostTableIsEmpty()
    {
        // Arrange
        await HttpClient.DeleteAsync($"{BlogPostEndpoint}/1");
        await HttpClient.DeleteAsync($"{BlogPostEndpoint}/2");
        
        // Act
        var response = await HttpClient.GetAsync($"{BlogPostEndpoint}?Page=1&PageSize=10");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<PagedList<BlogPostDto>>();
        result?.Items.Should().BeEmpty();
    }
}