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


// CORS�|���V�[�̖��O���`
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// CORS�|���V�[��ǉ�
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          // ������Angular�A�v����URL�𐳊m�ɋL�q
                          // ��: Angular��http://localhost:4200�œ����Ă���ꍇ
                          builder.WithOrigins("http://localhost:4200",
                                              "https://localhost:4200") // �K�v�ł����HTTPS��
                                 .AllowAnyHeader() // �S�Ẵw�b�_�[������ (�J���p)
                                 .AllowAnyMethod(); // �S�Ă�HTTP���\�b�h������ (�J���p)
                                                    // .AllowCredentials(); // �N�b�L�[��F�؃w�b�_�[�𑗂�ꍇ�͂�����K�v
                      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
