using System.Net;
using System.Net.Http.Json;
using Application.Features.BlogPosts.DTOs;
using Domain.Entities;
using FluentAssertions;
using WebApi.FunctionalTests.Abstractions;

namespace WebApi.FunctionalTests.BlogPosts;

public class GetBlogPostByIdTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
{
    [Fact]
    public async Task Should_ReturnOk_WhenBlogPostExists()
    {
        // Arrange
        var id = await CreateBlogPostAsync();

        // Act
        var response = await HttpClient.GetAsync($"{BlogPostEndpoint}/{id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<BlogPost>();
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task Should_ReturnNotFound_WhenBlogPostIsNotInDatabase()
    {
        // Act
        var response = await HttpClient.GetAsync($"{BlogPostEndpoint}/10");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}