using System.Net;
using System.Net.Http.Json;
using Application.DTOs.BlogPosts;
using Application.Features.BlogPosts.Commands.CreateBlogPost;
using FluentAssertions;
using WebApi.FunctionalTests.Abstractions;

namespace WebApi.FunctionalTests.BlogPosts;

public class CreateBlogPostTest(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
{
    [Fact]
    public async Task Should_ReturnSuccess()
    {
        // Arrange
        var blogPostDto = new CreateBlogPostDto("This is the title", "This is the description");
        var request = new CreateBlogPostCommand(blogPostDto);

        // Act
        var response = await HttpClient.PostAsJsonAsync("api/blogpost", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task Should_ReturnBadRequest_WhenTitleIsMissing()
    {
        // Arrange
        var blogPostDto = new CreateBlogPostDto("", "This is the description");
        var request = new CreateBlogPostCommand(blogPostDto);

        // Act
        var response = await HttpClient.PostAsJsonAsync("api/blogpost", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task Should_ReturnBadRequest_WhenDescriptionIsMissing()
    {
        // Arrange
        var blogPostDto = new CreateBlogPostDto("This is the title", "");
        var request = new CreateBlogPostCommand(blogPostDto);

        // Act
        var response = await HttpClient.PostAsJsonAsync("api/blogpost", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}