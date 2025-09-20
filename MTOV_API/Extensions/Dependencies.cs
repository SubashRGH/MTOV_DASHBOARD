namespace RECRM_API.Extensions
{
    using Asp.Versioning;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using MTOV_API.Middlewares;
    using MTOV_Application.Interfaces;
    using MTOV_Application.Mapper;
    using MTOV_Application.Services;
    using MTOV_AppSettings;
    using MTOV_Domain.Interface;
    using MTOV_Infrastructure.Repositiries;
    using MTOV_JwtTokenProvider;
    using MTOV_Utility.Interfaces;
    using MTOV_Utility.Services;
    using Serilog;

    /// <summary>
    /// Defines the <see cref="Dependencies" />
    /// </summary>
    public static class Dependencies
    {
        /// <summary>
        /// The AddCoreDependencies
        /// </summary>
        /// <param name="services">The services<see cref="IServiceCollection"/></param>
        public static void AddCoreDependencies(this IServiceCollection services)
        {
            services.AddAuthorizationBuilder()
                .SetDefaultPolicy(new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build());

            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = ApiVersion.Default;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new MediaTypeApiVersionReader("version"),
                    new MediaTypeApiVersionReader("x-version"),
                    new MediaTypeApiVersionReader("api-version"),
                    new MediaTypeApiVersionReader("ver")
                    );

                options.ReportApiVersions = true;
            }).AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddRateLimiter();
            services.AddSerilog();

            services.AddAutoMapper(typeof(MapperProfile));
        }

        /// <summary>
        /// The AddServiceDependencies
        /// </summary>
        /// <param name="services">The services<see cref="IServiceCollection"/></param>
        public static void AddServiceDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<AppProperty, AppProperty>();
            services.AddScoped<IInternalRequestHandler, InternalRequestHandler>();
            services.AddAuthenticationService(opt =>
            {
                var jwtSection = configuration.GetSection("JWT");
                opt.Issuer = jwtSection["Issuer"];
                opt.Audience = jwtSection["Audience"];
                opt.SecretKey = jwtSection["SecretKey"];
                opt.TimeOut = jwtSection["Timeout"];
            });

            #region Services

            services.AddScoped<IAccountServices, AccountServices>();
            services.AddScoped<ITradesServices, TradesServices>();
            services.AddScoped<IAuthServices, AuthServices>();

            #endregion Services

            #region Repository

            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ITradesRepository, TradesRepository>();

            #endregion Repository
        }

        /// <summary>
        /// The AddAppDependecies
        /// </summary>
        /// <param name="app">The app<see cref="WebApplication"/></param>
        public static void AddAppDependecies(this WebApplication app)
        {
            //if (app.Environment.IsDevelopment())
            //{
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                foreach (var description in app.DescribeApiVersions())
                {
                    options.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant()
                    );
                }
            });

            app.UseHsts();
            app.UseStaticFiles();
            app.UseSerilogRequestLogging();
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseCors(x => x
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseRateLimiter();
            app.MapControllers();
        }
    }
}