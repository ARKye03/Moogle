using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MoogleEngine;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS services.
builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowMyOrigin",
    builder => builder.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use CORS policy.
app.UseCors("AllowMyOrigin");

app.UseAuthorization();

app.MapControllers();

Moogle.LetsGetStarted("./src/zgraph/content");

app.Run();
