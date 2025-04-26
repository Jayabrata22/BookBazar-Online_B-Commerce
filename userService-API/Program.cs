using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using userService_API.DBContext;
using userService_API.Interfaces;
using userService_API.Repository;
using userService_API.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
// Add services to the container.
builder.Services.AddControllersWithViews();

//injection DB
builder.Services.AddDbContext<UserDBContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("BookBaazar_UserDb")));

//Inject Services
builder.Services.AddScoped<UserService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});


builder.Services.AddEndpointsApiExplorer();

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

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//app.MapGet("/weatherforecast", () =>
//{
//})
//.WithName("GetWeatherForecast");

app.Run();


