using System.Net.Http.Headers;
using System.Net.Http.Json;
using Domain.Shared;
using Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;
using Respawn;
using Testcontainers.PostgreSql;
using WebApi.FunctionalTests.Contracts;

namespace WebApi.FunctionalTests.Abstractions;

public class FunctionalTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:16.2")
        .WithDatabase("cleanproject")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .Build();

    private readonly PostgreSqlContainer _dbIdentityContainer = new PostgreSqlBuilder()
        .WithImage("postgres:16.2")
        .WithDatabase("identity")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .Build();

    private NpgsqlConnection _dbConnection = null!;
    private NpgsqlConnection _dbIdentityConnection = null!;
    private Respawner _applicationRespawner = null!;
    private Respawner _identityRespawner = null!;
    public HttpClient AuthorizedHttpClient { get; private set; } = null!;
    public HttpClient UnauthorizedHttpClient { get; private set; } = null!;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<ApplicationDbContext>));
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(_dbContainer.GetConnectionString()));
            services.RemoveAll(typeof(DbContextOptions<IdentityDataContext>));
            services.AddDbContext<IdentityDataContext>(options =>
                options.UseNpgsql(_dbIdentityContainer.GetConnectionString()));
        });
    }

    public new async Task DisposeAsync() => await _dbContainer.DisposeAsync();

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        await _dbIdentityContainer.StartAsync();
        _dbConnection = new NpgsqlConnection(_dbContainer.GetConnectionString());
        _dbIdentityConnection = new NpgsqlConnection(_dbIdentityContainer.GetConnectionString());
        AuthorizedHttpClient = CreateClient();
        UnauthorizedHttpClient = CreateClient();
        _identityRespawner = await InitializeRespawner(_dbIdentityConnection);
        _applicationRespawner = await InitializeRespawner(_dbConnection);
        var bearer = await GetBearer(AuthorizedHttpClient);
        AuthorizedHttpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", bearer);
    }

    private static async Task<string> GetBearer(HttpClient httpClient)
    {
        var request = new UserLoginRequest("cleanproject@gmail.com", "Cleanproject1@");
        await httpClient.PostAsJsonAsync("/register", request);
        var result = await httpClient.PostAsJsonAsync("/login", request);
        var response = await result.Content.ReadFromJsonAsync<UserLoginResponse>();
        Ensure.NotNullOrEmpty(response?.AccessToken);
        return response.AccessToken;
    }

    private static async Task<Respawner> InitializeRespawner(NpgsqlConnection dbConnection)
    {
        await dbConnection.OpenAsync();
        return await Respawner.CreateAsync(dbConnection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.Postgres,
            SchemasToInclude = ["public"]
        });
    }

    public async Task ResetApplicationDatabaseAsync()
    {
        await _applicationRespawner.ResetAsync(_dbConnection);
    }

    public async Task ResetIdentityDatabaseAsync()
    {
        await _identityRespawner.ResetAsync(_dbIdentityConnection);
    }
}