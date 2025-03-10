using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using CrayonCloudSales.Core.Interfaces;
using CrayonCloudSales.Infrastructure.Data;
using CrayonCloudSales.Infrastructure.Repositories;
using CrayonCloudSales.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Crayon Cloud Sales API", Version = "v1" });
});

// Add DB context
builder.Services.AddDbContext<CloudSalesDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repositories
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ISoftwareLicenseRepository, SoftwareLicenseRepository>();

// Register CCP service
builder.Services.AddHttpClient<ICCPService, CCPService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["CCPService:BaseUrl"] ?? "https://ccp-api.example.com");
    // Add any default headers, timeouts, etc.
});

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("DefaultPolicy", policy =>
    {
        policy.WithOrigins(builder.Configuration.GetSection("AllowedOrigins").Get<string[]>() ?? Array.Empty<string>())
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    // Apply migrations and seed data in development
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<CloudSalesDbContext>();
        dbContext.Database.Migrate();
    }
}

app.UseHttpsRedirection();
app.UseCors("DefaultPolicy");
app.UseAuthorization();
app.MapControllers();

app.Run();
