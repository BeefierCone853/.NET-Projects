using System.Net.Http.Json;
using Application.Features.BlogPosts.DTOs;

namespace WebApi.FunctionalTests.Abstractions;

public abstract class BaseFunctionalTest(FunctionalTestWebAppFactory factory)
{
    protected HttpClient AuthorizedHttpClient { get; } = factory.AuthorizedHttpClient;
    protected HttpClient UnauthorizedHttpClient { get; } = factory.UnauthorizedHttpClient;
    protected const string BlogPostEndpoint = "api/v1/blogposts";
    protected const string LoginEndpoint = "login";
    protected const string RegisterEndpoint = "register";
    protected readonly Func<Task> ResetApplicationDatabase = factory.ResetApplicationDatabaseAsync;
    protected readonly Func<Task> ResetIdentityDatabase = factory.ResetIdentityDatabaseAsync;
    protected const string FirstTitle = "abc";
    protected const string FirstDescription = "abc";
    protected const string SecondTitle = "bca";
    protected const string SecondDescription = "bca";

    protected async Task<int> CreateBlogPostAsync(
        string title = "This is the title",
        string description = "This is the description")
    {
        var request = new CreateBlogPostDto(title, description);
        var response = await AuthorizedHttpClient.PostAsJsonAsync($"{BlogPostEndpoint}", request);
        return await response.Content.ReadFromJsonAsync<int>();
    }

    protected async Task CreateBlogPostsAsync()
    {
        await CreateBlogPostAsync(FirstTitle, FirstDescription);
        await CreateBlogPostAsync(SecondTitle, SecondDescription);
    }
}