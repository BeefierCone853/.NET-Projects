using Application;
using Infrastructure;
using Persistence;
using Serilog;
using WebApi.Extensions;
using WebApi.Infrastructure;
using WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfig) =>
    loggerConfig.ReadFrom.Configuration(context.Configuration));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);
builder.Services.AddCors(o =>
{
    o.AddPolicy(
        "CorsPolicy",
        b => b.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.UseHttpsRedirection();

app.UseMiddleware<RequestLogContextMiddleware>();

app.UseSerilogRequestLogging();

app.UseAuthorization();

app.UseExceptionHandler();

app.MapControllers();

app.UseCors("CorsPolicy");

app.Run();

public partial class Program;