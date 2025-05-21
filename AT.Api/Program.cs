using AT.Api.Middlewares;
using AT.Application.Interfaces;
using AT.Application.Services;
using AT.Domain.Interfaces;
using AT.Infrastructure.Context;
using AT.Infrastructure.Initialize;
using AT.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ATDbContext>(opt => opt.UseInMemoryDatabase("ATDb"));
builder.Services.AddScoped<IApplicationRepository, ApplicationRepository>();
builder.Services.AddScoped<IApplicationStatusRepository, ApplicationStatusRepository>();
builder.Services.AddScoped<IApplicationService, ApplicationService>();
builder.Services.AddScoped<IApplicationStatusService, ApplicationStatusService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "AT API",
        Description = "A RESTful API for managing job applications",
        Contact = new OpenApiContact
        {
            Name = "Peter Pang",
            Email = "peter.pang.nz@gmail.com"
        }
    });
});


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Initialize Data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await DataGenerator.InitializeCodeTableData(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "AT API v1");
        options.RoutePrefix = string.Empty;
    });
}

//Add Middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
