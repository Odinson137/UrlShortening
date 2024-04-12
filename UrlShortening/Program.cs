using Microsoft.EntityFrameworkCore;
using UrlShortening.Data;
using UrlShortening.Interfaces;
using UrlShortening.Repositories;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

// Add services to the container.

services.AddScoped<IUrlRepository, UrlRepository>();
services.AddScoped<AutoMigration>();

services.AddControllersWithViews();


services.AddDbContext<DataContext>(
    dbContextOptions =>
    {
        var serverVersion = new MySqlServerVersion(new Version(10, 3, 0));
        var connectionString = configuration.GetConnectionString("MariaDb");
        dbContextOptions
            .UseMySql(connectionString, serverVersion)
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// вызов автомиграции
using (var scope = app.Services.CreateScope()) {
    await scope.ServiceProvider.GetRequiredService<AutoMigration>().Migrate();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();