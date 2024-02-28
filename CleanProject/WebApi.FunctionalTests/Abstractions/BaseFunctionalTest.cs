using System.Net.Http.Json;
using Application.Features.BlogPosts.DTOs;

namespace WebApi.FunctionalTests.Abstractions;

public class BaseFunctionalTest(FunctionalTestWebAppFactory factory) : IClassFixture<FunctionalTestWebAppFactory>
{
    protected HttpClient HttpClient { get; } = factory.CreateClient();
    protected const string BlogPostEndpoint = "api/v1/blogposts";
    protected const string FirstTitle = "abc";
    protected const string FirstDescription = "abc";
    protected const string SecondTitle = "bca";
    protected const string SecondDescription = "bca";

    protected async Task<int> CreateBlogPostAsync()
    {
        var request = new CreateBlogPostDto("This is the title", "This is the description");
        var response = await HttpClient.PostAsJsonAsync($"{BlogPostEndpoint}", request);
        return await response.Content.ReadFromJsonAsync<int>();
    }
}