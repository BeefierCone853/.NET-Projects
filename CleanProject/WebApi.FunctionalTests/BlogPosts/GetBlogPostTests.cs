using System.Net;
using System.Net.Http.Json;
using Application.Features.BlogPosts.DTOs;
using Domain.Entities;
using FluentAssertions;
using WebApi.FunctionalTests.Abstractions;

namespace WebApi.FunctionalTests.BlogPosts;

public class GetBlogPostTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
{
    [Fact]
    public async Task Should_ReturnOk_WhenBlogPostExists()
    {
        // Arrange
        var request = new CreateBlogPostDto("This is the title", "This is the description");
        await HttpClient.PostAsJsonAsync("api/blogpost", request);

        // Act
        var response = await HttpClient.GetAsync("api/blogpost/1");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<BlogPost>();
        result?.Title.Should().Be(request.Title);
        result?.Description.Should().Be(request.Description);
    }

    [Fact]
    public async Task Should_ReturnNotFound_WhenBlogPostNotInDatabase()
    {
        // Act
        var response = await HttpClient.GetAsync("api/blogpost/1");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}