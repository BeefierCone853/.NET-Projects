using System.Net;
using System.Net.Http.Json;
using Application.DTOs.BlogPosts;
using FluentAssertions;
using WebApi.FunctionalTests.Abstractions;
using WebApi.FunctionalTests.Contracts;
using WebApi.FunctionalTests.Extensions;

namespace WebApi.FunctionalTests.BlogPosts;

public class CreateBlogPostTest(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
{
    [Fact]
    public async Task Should_ReturnOk_WhenRequestIsValid()
    {
        // Arrange
        var request = new CreateBlogPostDto("This is the title", "This is the description");

        // Act
        var response = await HttpClient.PostAsJsonAsync("api/blogpost", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Should_ReturnBadRequest_WhenTitleIsMissing()
    {
        // Arrange
        var request = new CreateBlogPostDto("", "This is the description");

        // Act
        var response = await HttpClient.PostAsJsonAsync("api/blogpost", request);
        CustomProblemDetails problemDetails = await response.GetProblemDetails();
        problemDetails.Errors
            .Select(error => error.PropertyName)
            .Should()
            .Contain("Title");
        problemDetails.Errors
            .Select(error => error.ErrorMessage)
            .Should()
            .Contain("'Title' must not be empty.");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Should_ReturnBadRequest_WhenDescriptionIsMissing()
    {
        // Arrange
        var request = new CreateBlogPostDto("This is the title", "");

        // Act
        var response = await HttpClient.PostAsJsonAsync("api/blogpost", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        CustomProblemDetails problemDetails = await response.GetProblemDetails();
        problemDetails.Errors
            .Select(error => error.PropertyName)
            .Should()
            .Contain("Description");
        problemDetails.Errors
            .Select(error => error.ErrorMessage)
            .Should()
            .Contain("'Description' must not be empty.");
    }

    [Fact]
    public async Task Should_ReturnBadRequest_WhenTitleAndDescriptionAreMissing()
    {
        // Arrange
        var request = new CreateBlogPostDto("", "");

        // Act
        var response = await HttpClient.PostAsJsonAsync("api/blogpost", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        CustomProblemDetails problemDetails = await response.GetProblemDetails();
        problemDetails.Errors
            .Select(error => error.PropertyName)
            .Should()
            .Contain("Description");
        problemDetails.Errors
            .Select(error => error.ErrorMessage)
            .Should()
            .Contain("'Description' must not be empty.");
        problemDetails.Errors
            .Select(error => error.PropertyName)
            .Should()
            .Contain("Title");
        problemDetails.Errors
            .Select(error => error.ErrorMessage)
            .Should()
            .Contain("'Title' must not be empty.");
    }
}