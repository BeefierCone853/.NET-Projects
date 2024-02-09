using MVC.Abstractions;
using MVC.Profiles;
using MVC.Services;
using MVC.Services.Base;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<IClient, Client>(cl => cl.BaseAddress = new Uri("http://localhost:5056"));
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddSingleton<ILocalStorageService, LocalStorageService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();