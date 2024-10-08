using Asp.Versioning;
using Booking.Domain.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Reflection.Metadata.Ecma335;
using Amazon.Extensions.NETCore.Setup;
using Amazon.S3;
using Amazon;
namespace Booking.Api
{
    /// <summary>
    /// 
    /// </summary>
    public static class Startup
    {
        /// <summary>
        /// Authentication and Authorization
        /// </summary>
        public static void AddAuthenticationAndAuthorization(this IServiceCollection service, WebApplicationBuilder builder)
        { 
            service.AddAuthorization();
            service.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                var options = builder.Configuration.GetSection(JwtSettings.DefaultSection).Get<JwtSettings>();
                var jwtKey = options!.JwtKey;
                var issuer = options.Issuer;
                var audience = options.Audience;
                o.Authority = options.Authority;
                o.RequireHttpsMetadata = false;
                o.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
            });
            
        }
        /// <summary>
        /// Connection to Swagger 
        /// </summary>
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddApiVersioning();
                //.AddApiExplorer(options =>
                //{
                //    options.DefaultApiVersion = new ApiVersion(1, 0);
                //    options.GroupNameFormat = "'v'VVV";
                //    options.SubstituteApiVersionInUrl = true;
                //    options.AssumeDefaultVersionWhenUnspecified = true;
                //});
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "v1",
                    Title = "Boooking.API",
                    Description = "Version 1.0",
                    Contact = new OpenApiContact()
                    {
                        Name = "Oleg",
                        Email = "E-mail"
                    }
                });
                //options.SwaggerDoc("v2", new OpenApiInfo()
                //{
                //    Version = "v2",
                //    Title = "Boooking.API",
                //    Description = "Version 2.0",
                //    Contact = new OpenApiContact()
                //    {
                //        Name = "Oleg",
                //        Email = "E-mail"
                //    }
                //});

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    In = ParameterLocation.Header,
                    Description = "Enter valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        Array.Empty<String>()
                    }
                });
                //For the comments in XML
                var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
            });
        }
    }
}
