using BookService_API.Interface;
using BookService_API.Models;
using BookService_API.Repository;
using BookService_API.Service;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<BookDatabseContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("BookBaazar_BookDb")), ServiceLifetime.Scoped); ;

//Service Injection
builder.Services.AddTransient<BookService>();
builder.Services.AddTransient<ICustomerBookInterface, CustomerBookRepository>();
builder.Services.AddTransient<ISellerBookInterface, SellerBookRrepository>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options
        .WithTheme(ScalarTheme.Kepler)
        .WithDarkModeToggle(true)
        .WithClientButton(true);
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


