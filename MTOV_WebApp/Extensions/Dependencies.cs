namespace MTOV_WebApp.Extensions
{
    using MTOV_AppSettings;
    using MTOV_WebApp.Controller.Interfaces;
    using MTOV_WebApp.Controller.Services;

    /// <summary>
    /// dependency handler
    /// </summary>
    public static class Dependencies
    {
        public static void AddCoreDependencies(this IServiceCollection services)
        {
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<IDashboardApiService, DashboardApiService>();

            var property = new WebAppProperty();

            services.AddHttpContextAccessor();
            services.AddTransient<BearerTokenHandler>();

            // Add HttpClient for API calls
            services.AddHttpClient<IDashboardApiService, DashboardApiService>(client =>
            {
                // Configure the base address to point to our mock API
                client.BaseAddress = new Uri(property?.ApiConfigProp?.BaseUrl ?? ""); // This should match your application URL
                client.Timeout = TimeSpan.FromSeconds(property?.ApiConfigProp?.TimeOut ?? 0);
            }).AddHttpMessageHandler<BearerTokenHandler>();
        }
    }
}