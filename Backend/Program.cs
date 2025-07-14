using Backend.Repository;
using Backend.service;
using Microsoft.EntityFrameworkCore;
using WebApplication.Context;
using WebApplication.Repository;
using WebApplication.service;

var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<UserDataService>();
builder.Services.AddScoped<UserDataRepository>();
builder.Services.AddScoped<NewsletterService>();
builder.Services.AddScoped<NewsletterRepository>();
builder.Services.AddDbContext<AWSDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
