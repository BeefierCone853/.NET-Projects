using System.Net;
using FluentAssertions;
using WebApi.FunctionalTests.Abstractions;

namespace WebApi.FunctionalTests.BlogPosts;

public class DeleteBlogPostTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
{
    [Fact]
    public async Task Should_ReturnNoContent_WhenBlogPostWasDeleted()
    {
        // Arrange
        var id = await CreateBlogPostAsync();

        // Act
        var response = await HttpClient.DeleteAsync($"{BlogPostEndpoint}/{id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Should_ReturnNotFound_WhenBlogPostIsNotInDatabase()
    {
        // Arrange
        const int id = 1;

        // Act
        var response = await HttpClient.DeleteAsync($"{BlogPostEndpoint}/{id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}