namespace Codere.SBGOnline.API.Configurations
{
    #region Using

    using Codere.SBGOnline.Hipodromo.Domain.Repositories;
    using Codere.SBGOnline.Hipodromo.Infrastructure.Repositories;
    using Codere.SBGOnline.WebApi.Helpers;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using Newtonsoft.Json;
    using Polly;
    using Polly.Extensions.Http;
    using System;
    using System.Net.Http;
    using System.Text;

    #endregion

    public static class ServicesConfiguration
    {
        public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
        {
            services
                .AddScoped<IRepositoryUser, RepositoryUser>()
                .AddScoped<IRepositoryTransaction, RepositoryTransaction>();

            return services;
        }

        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            //services
            //    .AddScoped<IServiceBarcode, ServiceBarcode>()
            //    .AddScoped<IServiceEcho, ServiceEcho>();

            return services;
        }

        public static IServiceCollection AddMiddleware(this IServiceCollection services)
        {
            services.AddMvc().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = new ReferenceLoopHandling());

            return services;
        }

        public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSection = configuration.GetSection("Jwt");
            services.Configure<JwtAppSettings>(jwtSection);

            // configure jwt authentication
            var jwtAppSettings = jwtSection.Get<JwtAppSettings>();
            var key = Encoding.ASCII.GetBytes(jwtAppSettings.SecretKey);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            return services;
        }

        public static IServiceCollection AddCorsConfiguration(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", new Microsoft.AspNetCore.Cors.Infrastructure.CorsPolicyBuilder()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
                    .Build());
            });

        public static IServiceCollection AddPolly(this IServiceCollection services)
        {
            services.AddHttpClient("cajacodere")
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .AddPolicyHandler(GetRetryPolicy());

            return services;
        }

        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg =>
                    msg.StatusCode == System.Net.HttpStatusCode.NotFound ||
                    msg.StatusCode == System.Net.HttpStatusCode.BadGateway)
                .WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(30));
        }
    }
}