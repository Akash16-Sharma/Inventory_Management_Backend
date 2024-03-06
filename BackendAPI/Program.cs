using BackendAPI;
using BackendAPI.IRepository;
using BackendAPI.Models;
using BackendAPI.Models.Class;
using BackendAPI.Repository;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var connectionString = configuration.GetConnectionString("SQLDatabase");
if (string.IsNullOrEmpty(connectionString))
{
    throw new ApplicationException("SQLDatabase connection string is missing in appsettings.json");
}

builder.Services.AddDbContextPool<DataContext>(options =>
    options.UseSqlServer(connectionString));

// Remove unnecessary registration of AppSettings
// builder.Services.AddScoped<BackendAPI.Models.Class.AppSettings>();

// Add the interface registration method
builder.Services.AddScoped<IItemRepo, ItemRepo>();
builder.Services.AddInterfaceRepo();
builder.Services.Configure<AppSettings>(configuration.GetSection("AppSettings"));

var app = builder.Build();

// Enable CORS
app.UseCors(options =>
{
    options.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});

app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
