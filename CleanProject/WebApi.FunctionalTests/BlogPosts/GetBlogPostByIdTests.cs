﻿using System.Net;
using System.Net.Http.Json;
using Domain.Features.BlogPosts;
using FluentAssertions;
using WebApi.FunctionalTests.Abstractions;

namespace WebApi.FunctionalTests.BlogPosts;

[Collection(nameof(SharedTestCollection))]
public class GetBlogPostByIdTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory), IAsyncLifetime
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
    public async Task Should_ReturnUnauthorized_WhenCredentialsNotProvided()
    {
        // Act
        var response = await HttpClient.GetAsync($"{BlogPostEndpoint}/10");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Should_ReturnNotFound_WhenBlogPostIsNotInDatabase()
    {
        // Act
        var response = await HttpClient.GetAsync($"{BlogPostEndpoint}/10");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    public Task InitializeAsync() => ResetDatabase();

    public Task DisposeAsync() => Task.CompletedTask;
}