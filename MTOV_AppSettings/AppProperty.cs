using Microsoft.Extensions.Configuration;

namespace MTOV_AppSettings
{
    public class AppProperty
    {
        public AppProperty(IConfiguration config)
        {
            // usually we get these info from AWS or Azure Key Vault
            var configurationBuilder = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory);

            configurationBuilder.AddJsonFile("appsettings.json", false);

            var root = configurationBuilder.Build();

            var appSettings = config.GetSection("AppSetting");
            {
                AuthUserName = appSettings.GetSection("AppUserName").Value;
                AuthPass = appSettings.GetSection("AppPassword").Value;
            }
        }

        public string? AuthUserName { get; set; }
        public string? AuthPass { get; set; }
    }
}
