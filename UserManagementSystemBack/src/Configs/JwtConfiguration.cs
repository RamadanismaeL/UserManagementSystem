using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace UserManagementSystemBack.src.Configs
{
    /// <summary>
    /// Json Web Token Configuration - Add Authentication
    /// </summary>
    public static class JwtConfiguration
    {
        /// <summary>
        /// This is a method to authentication
        /// </summary>
        /// <param name="service"></param>
        /// <param name="configuration"></param>
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
        }
    }
}