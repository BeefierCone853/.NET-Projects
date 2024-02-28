using System.Net.Http.Json;
using Application.Features.BlogPosts.DTOs;

namespace WebApi.FunctionalTests.Abstractions;

public abstract class BaseFunctionalTest(FunctionalTestWebAppFactory factory)
{
    protected HttpClient HttpClient { get; } = factory.HttpClient;
    protected const string BlogPostEndpoint = "api/v1/blogposts";
    protected readonly Func<Task> ResetDatabase = factory.ResetDatabaseAsync;
    protected const string FirstTitle = "abc";
    protected const string FirstDescription = "abc";
    protected const string SecondTitle = "bca";
    protected const string SecondDescription = "bca";

    protected async Task<int> CreateBlogPostAsync(
        string title = "This is the title",
        string description = "This is the description")
    {
        var request = new CreateBlogPostDto(title, description);
        var response = await HttpClient.PostAsJsonAsync($"{BlogPostEndpoint}", request);
        return await response.Content.ReadFromJsonAsync<int>();
    }

    protected async Task CreateBlogPostsAsync()
    {
        await CreateBlogPostAsync(FirstTitle, FirstDescription);
        await CreateBlogPostAsync(SecondTitle, SecondDescription);
    }
}