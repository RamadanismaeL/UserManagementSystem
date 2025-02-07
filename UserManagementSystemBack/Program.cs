using Microsoft.EntityFrameworkCore;
using UserManagementSystemBack.src.Data;
using UserManagementSystemBack.src.Configs;
using UserManagementSystemBack.src.Interfaces;
using UserManagementSystemBack.src.Repositories;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

string getUserFront = configuration["Origins:userFront"] ?? throw new InvalidOperationException("Origins:userFront is not set");
//Console.WriteLine($"Origins:userFront: {getUserFront}");
var getUserName = Environment.GetEnvironmentVariable("DB_USERNAME") ?? throw new InvalidOperationException("DB_USERNAME is not set");
var getPassword = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? throw new InvalidOperationException("DB_PASSWORD is not set");
var getPort = Environment.GetEnvironmentVariable("DB_PORT") ?? throw new InvalidOperationException("DB_PORT is not set");
var getServer = Environment.GetEnvironmentVariable("DB_SERVER") ?? throw new InvalidOperationException("DB_SERVER is not set");
//Console.WriteLine($"DB_USERNAME: {getUserName} \n DB_PASSWORD: {getPassword} \n DB_PORT: {getPort} \n DB_SERVER: {getServer}");
string connect = $"server={getServer}; port={getPort}; database=db_user_management_system; user={getUserName}; password={getPassword}; Persist Security Info=False; Connect Timeout=300";

builder.Services.AddDbContextPool<UserManagementSystem_db>(ram => ram.UseMySql(connect, ServerVersion.AutoDetect(connect)));
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddAutoMapper(typeof(UserMap));

builder.Services.AddCors(options =>
{
    options.AddPolicy("UserManagementSystemFront", builder => builder
        .WithMethods("GET", "POST", "PUT", "PATCH", "DELETE")
        .AllowAnyHeader()
        .WithOrigins(getUserFront)
        );
});
builder.Services.AddJwtAuthentication(configuration);
builder.Services.AddAuthorization();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerConfiguration();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();