using backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using backend.Application.Interfaces.Repositories;
using backend.Infrastructure.Repositories;
using backend.Application.Interfaces;
using backend.Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens; 
using System.Text; 

var builder = WebApplication.CreateBuilder(args);

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // frontend URL
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Add services to the container.
builder.Services.AddControllers();

// Register DbContext with SQL Server
builder.Services.AddDbContext<MaintenanceDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Generic Repository
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IServiceService, ServiceService>();

builder.Services.AddSingleton<JwtService>();

// Configure JWT Authentication
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // for development
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
});

var app = builder.Build();

// Use CORS
app.UseCors("AllowFrontend");

// **Run EF Core migrations automatically on startup**
using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<MaintenanceDbContext>();

var retries = 10;
var delay = TimeSpan.FromSeconds(5);

while (retries > 0)
{
    try
    {
        db.Database.Migrate();
        break;  // success
    }
    catch (Exception ex)
    {
        retries--;
        if (retries == 0)
        {
            Console.WriteLine("Failed to migrate database: " + ex.Message);
            throw;  // or handle gracefully/log and continue
        }
        Console.WriteLine("Migration failed, retrying in 5 seconds...");
        await Task.Delay(delay);
    }
}
//

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseAuthentication(); //Must come before UseAuthorization
app.UseAuthorization();

app.MapControllers();

app.Run();
