namespace WebApi.FunctionalTests.Abstractions;

public class BaseFunctionalTest(FunctionalTestWebAppFactory factory) : IClassFixture<FunctionalTestWebAppFactory>
{
    protected HttpClient HttpClient { get; } = factory.CreateClient();
    protected const string BlogPostEndpoint = "api/blogposts";
}