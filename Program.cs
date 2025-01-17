

using Microsoft.EntityFrameworkCore;
using userManagementSystemBack.src.Data;


/**
** @author Ramadan Ismael
*/
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var username = Environment.GetEnvironmentVariable("DB_USER") ?? throw new InvalidOperationException("DB_USER is not set");
var password = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? throw new InvalidOperationException("DB_PASSWORD is not set");
var port = Environment.GetEnvironmentVariable("DB_PORT") ?? throw new InvalidOperationException("DB_PORT is not set");
var server = Environment.GetEnvironmentVariable("DB_SERVER") ?? throw new InvalidOperationException("DB_SERVER is not set");

string? connect = $"server={server}; port={port}; database=dbUserManagementSystem; user={username}; password={password}; Persist Security Info=false; Connect Timeout=300";
builder.Services.AddDbContextPool<DataContext>(ram => ram.UseMySql(connect, ServerVersion.AutoDetect(connect)));

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.Run();