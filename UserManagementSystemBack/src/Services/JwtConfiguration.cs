using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

/**
** @author Ramadan Ismael
*/
namespace UserManagementSystemBack.src.Services
{
    public static class JwtConfiguration
    {
        public static void AddJwtAuthentication(this IServiceCollection service, IConfiguration configuration)
        {
            string getAudience = configuration["JwtSettings:Audience"] ?? throw new InvalidOperationException("JwtSettings:Audience is not set");
            var getSigningKey = Environment.GetEnvironmentVariable("JWT_SIGNING_KEY") ?? throw new InvalidOperationException("JWT_SIGNING_KEY is not set");
            var getIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? throw new InvalidOperationException("JWT_ISSUER is not set");

            //Console.WriteLine($"JwtSettings:Audience: {getAudience} \nJWT_SIGNING_KEY: {getSigningKey} \n JWT_ISSUER: {getIssuer}");
            
            service.AddAuthentication(ram =>
            {
                ram.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                ram.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(ram =>
            {
                ram.RequireHttpsMetadata = true;
                ram.SaveToken = true;
                ram.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = getIssuer,

                    ValidateAudience = true,
                    ValidAudience = getAudience,
                    RequireAudience = true,

                    ValidateLifetime = true,
                    RequireExpirationTime = true,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(getSigningKey)),
                    RequireSignedTokens = true,
                    ClockSkew = TimeSpan.FromMilliseconds(900)
                };
            });
            service.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Cookie.HttpOnly = true;        // Impede o acesso via JavaScript
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Só envia cookies em conexões HTTPS
                options.Cookie.SameSite = SameSiteMode.Strict; // Previne CSRF
                options.Cookie.Name = "UserManagementSystemCookie";    // Nome personalizado para o cookie
                options.Cookie.MaxAge = TimeSpan.FromMinutes(30);  // Expira após 30 minutos de inatividade
                options.SlidingExpiration = true;  // Renova a expiração a cada requisição
                options.Events.OnSigningOut = context =>
                {
                    context.Response.Cookies.Delete("UserManagementSystemCookie");
                    return Task.CompletedTask;
                };
            });
        }
    }
}