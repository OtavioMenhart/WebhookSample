using Microsoft.OpenApi.Models;
using System.Reflection;
using WebhookSample.API.Extensions;
using WebhookSample.API.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(opt => opt.Filters.Add(typeof(GlobalExceptionFilters)));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    foreach (string filePath in
             Directory.GetFiles(
                 Path.Combine(
                     Path.GetDirectoryName(
                         Assembly.GetExecutingAssembly().Location) ?? string.Empty), "*.xml"))
    {
        opt.IncludeXmlComments(filePath);
    }

    opt.MapType<DateOnly>(() => new OpenApiSchema { Type = "string", Format = "date" });
});

builder.Services.ConfigureServices();
builder.Services.ConfigureRepositories(builder.Configuration);
builder.Services.ConfigureRabbit(builder.Configuration);
builder.Host.ConfigureLog();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
