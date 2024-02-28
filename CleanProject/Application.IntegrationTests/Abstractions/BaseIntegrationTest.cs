using Application.Features.BlogPosts.Commands.CreateBlogPost;
using Application.Features.BlogPosts.DTOs;
using Application.Helpers;
using Domain.Shared;
using Infrastructure.Data;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.IntegrationTests.Abstractions;

public class BaseIntegrationTest
{
    protected readonly ISender Sender;
    protected readonly ApplicationDbContext DbContext;
    protected readonly Func<Task> ResetDatabase;
    protected static readonly SearchQuery SearchQuery = new(
        null,
        null,
        null,
        1,
        10);

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        var scope = factory.Services.CreateScope();
        Sender = scope.ServiceProvider.GetRequiredService<ISender>();
        DbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        ResetDatabase = factory.ResetDatabaseAsync;
    }

    protected async Task<Result<int>> CreateBlogPostAsync(string title = "Title", string description = "description")
    {
        var blogPostDto = new CreateBlogPostDto(title, description);
        var command = new CreateBlogPostCommand(blogPostDto);
        return await Sender.Send(command);
    }

    protected async Task CreateBlogPostsAsync()
    {
        var blogPostDto = new CreateBlogPostDto("Title", "Description");
        var command = new CreateBlogPostCommand(blogPostDto);
        await Sender.Send(command);
        blogPostDto = new CreateBlogPostDto("Title2", "Description2");
        command = new CreateBlogPostCommand(blogPostDto);
        await Sender.Send(command);
    }
}