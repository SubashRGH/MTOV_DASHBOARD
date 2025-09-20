namespace MTOV_JwtTokenProvider
{
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using System;

    /// <summary>
    /// Defines the <see cref="JwtTokenProvideExtensions" />
    /// </summary>
    public static class JwtTokenProvideExtensions
    {
        /// <summary>
        /// The AddAuthenticationService
        /// </summary>
        /// <param name="services">The services<see cref="IServiceCollection"/></param>
        /// <param name="configureOptions">The configureOptions<see cref="Action{JwtTokenProviderOptionsBuilder}"/></param>
        /// <returns>The <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddAuthenticationService(
              this IServiceCollection services, Action<JwtTokenProviderOptionsBuilder> configureOptions
        )
        {
            // Bind JwtTokenProviderOptionsBuilder from configuration
            services.Configure(configureOptions);

            // Register JwtTokenProviderOptionsBuilder as a scoped for dependency injection
            services.AddScoped(resolver =>
                resolver.GetRequiredService<IOptions<JwtTokenProviderOptionsBuilder>>().Value
            );

            // Register Providers
            services.AddScoped<IJwtTokenProvider, JwtTokenProvider>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = services.BuildServiceProvider().GetRequiredService<IJwtTokenProvider>().GetValidationParameters();
            });
            return services;
        }

        /// <summary>
        /// The AddAuthorizationService
        /// </summary>
        /// <param name="services">The services<see cref="IServiceCollection"/></param>
        /// <returns>The <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddAuthorizationService(
          this IServiceCollection services
        )
        {
            services.AddAuthorizationBuilder()
                .SetDefaultPolicy(new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build());

            return services;
        }

        /// <summary>
        /// The UseAuthenticationService
        /// </summary>
        /// <param name="app">The app<see cref="WebApplication"/></param>
        public static void UseAuthenticationService(this WebApplication app)
        {
            app.UseAuthentication();
        }

        /// <summary>
        /// The UseAuthorizationService
        /// </summary>
        /// <param name="app">The app<see cref="WebApplication"/></param>
        public static void UseAuthorizationService(this WebApplication app)
        {
            app.UseAuthorization();
        }
    }
}
