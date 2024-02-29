using System.Net;
using System.Net.Http.Json;
using Application.Features.BlogPosts;
using Application.Features.BlogPosts.DTOs;
using FluentAssertions;
using WebApi.FunctionalTests.Abstractions;
using WebApi.FunctionalTests.Contracts;
using WebApi.FunctionalTests.Extensions;

namespace WebApi.FunctionalTests.BlogPosts;

[Collection(nameof(SharedTestCollection))]
public class CreateUpdateBlogPostTests(FunctionalTestWebAppFactory factory)
    : BaseFunctionalTest(factory), IAsyncLifetime
{
    [Fact]
    public async Task Should_ReturnOk_WhenRequestIsValid()
    {
        // Arrange
        var request = new CreateBlogPostDto("This is the title", "This is the description");

        // Act
        var response = await HttpClient.PostAsJsonAsync(BlogPostEndpoint, request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var blogPostId = await response.Content.ReadFromJsonAsync<int>();
        blogPostId.Should().Be(1);
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
    public async Task Should_ReturnUnauthorized_WhenCredentialsNotProvided()
    {
        // Arrange
        var request = new CreateBlogPostDto("This is the title", "This is the description");

        // Act
        var response = await HttpClient.PostAsJsonAsync(BlogPostEndpoint, request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
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

    public Task InitializeAsync() => ResetDatabase();

    public Task DisposeAsync() => Task.CompletedTask;
}