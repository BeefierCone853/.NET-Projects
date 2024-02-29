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

namespace Application.IntegrationTests.Abstractions;

public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
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
    private Respawner _respawner = null!;
    public HttpClient HttpClient { get; private set; } = null!;

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

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        await _dbIdentityContainer.StartAsync();
        _dbConnection = new NpgsqlConnection(_dbContainer.GetConnectionString());
        HttpClient = CreateClient();
        await InitializeRespawner(_dbConnection);
    }
    
    private async Task InitializeRespawner(NpgsqlConnection dbConnection)
    {
        await dbConnection.OpenAsync();
        _respawner = await Respawner.CreateAsync(dbConnection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.Postgres,
            SchemasToInclude = ["public"]
        });
    }
    
    public new async Task DisposeAsync() => await _dbContainer.DisposeAsync();
    
    public async Task ResetDatabaseAsync()
    {
        await _respawner.ResetAsync(_dbConnection);
    }
}