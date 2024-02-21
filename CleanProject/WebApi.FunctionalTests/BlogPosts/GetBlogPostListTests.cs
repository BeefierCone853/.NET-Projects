using System.Net;
using System.Net.Http.Json;
using Application.Features.BlogPosts.DTOs;
using Domain.Entities;
using FluentAssertions;
using WebApi.FunctionalTests.Abstractions;

namespace WebApi.FunctionalTests.BlogPosts;

public class GetBlogPostListTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
{
    [Fact]
    public async Task Should_ReturnOk_WithBlogPostDtos_WhenBlogPostExists()
    {
        // Arrange
        var request = new CreateBlogPostDto("This is the title", "This is the description");
        var blogPost1 = new BlogPostDto(request.Title, request.Description, 1);
        await HttpClient.PostAsJsonAsync("api/blogpost", request);
        request = new CreateBlogPostDto("This is the title2", "This is the description2");
        var blogPost2 = new BlogPostDto(request.Title, request.Description, 2);
        await HttpClient.PostAsJsonAsync("api/blogpost", request);

        // Act
        var response = await HttpClient.GetAsync("/api/BlogPost");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<List<BlogPostDto>>();
        result?.Count.Should().Be(2);
        if (result != null)
        {
            Assert.Contains(blogPost1, result);
            Assert.Contains(blogPost2, result);
        }
    }

    [Fact]
    public async Task Should_ReturnOk_WithEmptyList_WhenBlogPostTableIsEmpty()
    {
        // Act
        var response = await HttpClient.GetAsync("/api/BlogPost");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<List<BlogPostDto>>();
        result.Should().BeEmpty();
    }
}