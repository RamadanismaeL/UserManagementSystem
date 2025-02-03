using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

/**
** @author Ramadan Ismael
*/

var builder = WebApplication.CreateBuilder(args);
var getSigningKey = Environment.GetEnvironmentVariable("JWT_SIGNING_KEY") ?? throw new InvalidOperationException("JWT_SIGNING_KEY is not set");
var getIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? throw new InvalidOperationException("JWT_ISSUER is not set");
var getUserName = Environment.GetEnvironmentVariable("DB_USERNAME") ?? throw new InvalidOperationException("DB_USERNAME is not set");
var getPassword = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? throw new InvalidOperationException("DB_PASSWORD is not set");
var getPort = Environment.GetEnvironmentVariable("DB_PORT") ?? throw new InvalidOperationException("DB_PORT is not set");
var getServer = Environment.GetEnvironmentVariable("DB_SERVER") ?? throw new InvalidOperationException("DB_SERVER is not set");
Console.WriteLine($"JWT_SIGNING_KEY: {getSigningKey} \n JWT_ISSUER: {getIssuer} \n DB_USERNAME: {getUserName} \n DB_PASSWORD: {getPassword} \n DB_PORT: {getPort} \n DB_SERVER: {getServer}");

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(options =>
{
    ConfigureSwaggerDoc(options);
    ConfigureJwtAuthentication(options);
});
static void ConfigureSwaggerDoc(SwaggerGenOptions options)
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "User Management System",
        Version = "v1",
        Description = "A simple management system ASP.NET Core Web API",
        Contact = new OpenApiContact
        {
            Name = "Ramadan Ismael",
            Email = "ramadan.ismael02@gmail.com"
            //Url = new Uri("https://www.linkedin.com/in/ramadan-ismael-0b1b3b1b3/")
        }
    });
}

static void ConfigureJwtAuthentication(SwaggerGenOptions options)
{
    var securitySchema = new OpenApiSecurityScheme
    {
        Name = "JWT Authentication",
        Description = "Enter JWT Bearer token **_only_**",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securitySchema);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { securitySchema, new[] { "Bearer" } }
    });
}

var app = builder.Build();
if (app.Environment.IsDevelopment() || app.Environment.IsProduction ())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();