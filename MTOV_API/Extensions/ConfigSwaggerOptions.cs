namespace MTOV_API.Extensions
{
    using Asp.Versioning.ApiExplorer;
    using Microsoft.Extensions.Options;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;

    /// <summary>
    /// Defines the <see cref="ConfigSwaggerOptions" />
    /// </summary>
    public class ConfigSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        /// <summary>
        /// Defines the _provider
        /// </summary>
        private readonly IApiVersionDescriptionProvider _provider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigSwaggerOptions"/> class.
        /// </summary>
        /// <param name="provider">The provider<see cref="IApiVersionDescriptionProvider"/></param>
        public ConfigSwaggerOptions(IApiVersionDescriptionProvider provider) => _provider = provider;

        /// <summary>
        /// The Configure
        /// </summary>
        /// <param name="options">The options<see cref="SwaggerGenOptions"/></param>
        public void Configure(SwaggerGenOptions options)
        {
        }

        /// <summary>
        /// The CreateInfoForApiVersion
        /// </summary>
        /// <param name="description">The description<see cref="ApiVersionDescription"/></param>
        /// <returns>The <see cref="OpenApiInfo"/></returns>
        private OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description) => new()
        {
            Title = "MT5 ACC OVERVIEW API API",
            Version = description.ApiVersion.ToString(),
            Description = "Async API Provider"
        };
    }
}