using System.Net;
using System.Net.Http.Json;
using Application.Features.BlogPosts;
using Application.Features.BlogPosts.DTOs;
using FluentAssertions;
using WebApi.FunctionalTests.Abstractions;
using WebApi.FunctionalTests.Contracts;
using WebApi.FunctionalTests.Extensions;

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
        const int id = 10;
        var request = new UpdateBlogPostDto("This is the title", "This is the description");

        // Act
        var response = await HttpClient.PutAsJsonAsync($"{BlogPostEndpoint}/{id}", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Should_ReturnBadRequest_WhenTitleIsMissing()
    {
        // Arrange
        var request = new CreateBlogPostDto("", "This is the description");

        // Act
        var response = await HttpClient.PostAsJsonAsync(BlogPostEndpoint, request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        CustomProblemDetails problemDetails = await response.GetProblemDetails();
        problemDetails.Errors.Select(e => e.Code).Should()
            .Contain([BlogPostErrorCodes.SharedCreateUpdateBlogPost.MissingTitle]);
    }

    [Fact]
    public async Task Should_ReturnBadRequest_WhenDescriptionIsMissing()
    {
        // Arrange
        var request = new CreateBlogPostDto("This is the title", "");

        // Act
        var response = await HttpClient.PostAsJsonAsync(BlogPostEndpoint, request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        CustomProblemDetails problemDetails = await response.GetProblemDetails();
        problemDetails.Errors.Select(e => e.Code).Should()
            .Contain([BlogPostErrorCodes.SharedCreateUpdateBlogPost.MissingDescription]);
    }

    [Fact]
    public async Task Should_ReturnBadRequest_WhenTitleAndDescriptionAreMissing()
    {
        // Arrange
        var request = new CreateBlogPostDto("", "");

        // Act
        var response = await HttpClient.PostAsJsonAsync(BlogPostEndpoint, request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        CustomProblemDetails problemDetails = await response.GetProblemDetails();
        problemDetails.Errors.Select(e => e.Code).Should().Contain([
            BlogPostErrorCodes.SharedCreateUpdateBlogPost.MissingTitle,
            BlogPostErrorCodes.SharedCreateUpdateBlogPost.MissingDescription
        ]);
    }
}