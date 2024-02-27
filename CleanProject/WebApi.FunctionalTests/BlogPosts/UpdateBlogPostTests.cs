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
        var id = await CreateBlogPostAsync();
        var updateRequest = new UpdateBlogPostDto("New title", "New description");

        // Act
        var response = await HttpClient.PutAsJsonAsync($"{BlogPostEndpoint}/{id}", updateRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Should_ReturnNotFound_WhenBlogPostIsNotInDatabase()
    {
        // Arrange
        const int id = 1;
        var request = new UpdateBlogPostDto("This is the title", "This is the description");

        // Act
        var response = await HttpClient.PutAsJsonAsync($"{BlogPostEndpoint}/{id}", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}