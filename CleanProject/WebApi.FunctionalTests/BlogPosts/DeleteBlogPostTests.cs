using System.Net;
using System.Net.Http.Json;
using Application.Features.BlogPosts.DTOs;
using Domain.Features.BlogPosts;
using FluentAssertions;
using WebApi.FunctionalTests.Abstractions;
using WebApi.FunctionalTests.Contracts;
using WebApi.FunctionalTests.Extensions;

namespace WebApi.FunctionalTests.BlogPosts;

public class DeleteBlogPostTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
{
    [Fact]
    public async Task Should_ReturnNoContent_WhenBlogPostWasDeleted()
    {
        // Arrange
        var request = new CreateBlogPostDto("This is the title", "This is the description");
        await HttpClient.PostAsJsonAsync("api/blogpost", request);

        // Act
        var response = await HttpClient.DeleteAsync("api/blogpost/1");
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Should_ReturnNotFound_WhenBlogPostIsNotInDatabase()
    {
        // Arrange
        const int id = 1;

        // Act
        var response = await HttpClient.DeleteAsync($"api/blogpost/{id}");

        // Assert
        CustomDeleteProblemDetails problemDetails = await response.GetDeleteProblemDetails();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        problemDetails.Code.Should().Be(BlogPostsErrors.NotFound(id).Code);
        problemDetails.Description.Should().Be(BlogPostsErrors.NotFound(id).Description);
        problemDetails.Type.Should().Be(BlogPostsErrors.NotFound(id).Type);
    }
}