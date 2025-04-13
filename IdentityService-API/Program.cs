using IdentityService_API.models;
using IdentityService_API.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer; // Add this using directive
using Microsoft.IdentityModel.Tokens; // Add this using directive
using System.Text;
using Microsoft.EntityFrameworkCore;
using IdentityService_API.DBContext;
using Scalar.AspNetCore;
using userService_API.Interfaces;
using IdentityService_API.Repository; // Add this using directive

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// For calling UserService
builder.Services.AddHttpClient();

// Add services to the container.
builder.Services.AddControllersWithViews();
//injection DB
builder.Services.AddDbContext<IdentityServiceDBContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("BookBaazar_IdentityDb")));

//injection Dependency ijecrtion
builder.Services.AddScoped<AuthService>();
builder.Services.AddTransient<IidentityUser, IidentityuserRepository>();

builder.Services.AddControllers();

// Configure Identity
builder.Services.AddIdentity<Identity, IdentityRole>()
    .AddEntityFrameworkStores<IdentityServiceDBContext>()
    .AddDefaultTokenProviders();

var jwtKey = builder.Configuration["Jwt:Key"]!;
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });


var app = builder.Build();


builder.Services.AddEndpointsApiExplorer();

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGet("/weatherforecast", () =>
{

})
.WithName("GetWeatherForecast");

app.Run();
