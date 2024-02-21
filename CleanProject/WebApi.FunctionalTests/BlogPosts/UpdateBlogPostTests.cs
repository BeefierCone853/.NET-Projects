using System.Net;
using System.Net.Http.Json;
using Application.Features.BlogPosts.DTOs;
using FluentAssertions;
using WebApi.FunctionalTests.Abstractions;

namespace WebApi.FunctionalTests.BlogPosts;

public class UpdateBlogPostTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
{
    [Fact]
    public async Task Should_ReturnNoContent_WhenBlogPostWasUpdated()
    {
        // Arrange
        const int id = 1;
        var updateRequest = new UpdateBlogPostDto("New title", "New description");
        var createRequest = new CreateBlogPostDto("This is the title", "This is the description");
        await HttpClient.PostAsJsonAsync("api/blogpost", createRequest);

        // Act
        var response = await HttpClient.PutAsJsonAsync($"api/blogpost/{id}", updateRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Should_ReturnNotFound_WhenBlogPostIsNotInDatabase()
    {
        // Arrange
        const int id = 1;
        var request = new CreateBlogPostDto("This is the title", "This is the description");

        // Act
        var response = await HttpClient.PutAsJsonAsync($"api/blogpost/{id}", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}