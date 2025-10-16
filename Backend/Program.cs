﻿using Backend.Annotation;
using Backend.Repository;
using Backend.service;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using WebApplication.Context;
using WebApplication.Repository;
using WebApplication.service;

var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

// Add services to the container.
// DIするサービスを自動登録する
AppDomain.CurrentDomain.GetAssemblies();
foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
{
    builder.Services.RegisterComponents(asm.FullName!);
}

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


// CORSポリシーの名前を定義
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// CORSポリシーを追加
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          // ここにAngularアプリのURLを正確に記述
                          // 例: Angularがhttp://localhost:4200で動いている場合
                          builder.WithOrigins("http://localhost:4200",
                                              "https://localhost:4200") // 必要であればHTTPSも
                                 .AllowAnyHeader() // 全てのヘッダーを許可 (開発用)
                                 .AllowAnyMethod(); // 全てのHTTPメソッドを許可 (開発用)
                                                    // .AllowCredentials(); // クッキーや認証ヘッダーを送る場合はこれも必要
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


public class AWSDbContextFactory : IDesignTimeDbContextFactory<AWSDbContext>
{
    public AWSDbContext CreateDbContext(string[] args)
    {
        var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

        // ここで接続文字列や設定をハードコードするか、Configurationを読み込む
        var optionsBuilder = new DbContextOptionsBuilder<AWSDbContext>();

        // 例 SQL Serverの場合
        optionsBuilder.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
            ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection")));

        return new AWSDbContext(optionsBuilder.Options);
    }
}