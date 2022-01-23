using Dashboard.Core.Caching;
using Microsoft.EntityFrameworkCore;
using RedisExample.Core;
using RedisExample.Entities.DbContexts;
using RedisExample.Services;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json")
           .Build();

builder.Services.Configure<Config>(configuration.GetSection("Config"));
builder.Services.AddSingleton<IRedisCacheService, RedisCacheService>();

builder.Services.AddScoped<PermissonFilter>();
builder.Services.AddTransient<IPermissionService, PermissionService>();

var connectionString = builder.Configuration.GetConnectionString("SQLDBConnection");
builder.Services.AddDbContext<RedisDBContext>(x => x.UseSqlServer(connectionString));


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
