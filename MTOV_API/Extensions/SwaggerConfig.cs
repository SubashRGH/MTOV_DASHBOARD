namespace MTOV_API.Extensions
{
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.OpenApi.Models;
    using System.Reflection;

    /// <summary>
    /// Defines the <see cref="SwaggerConfig" />
    /// </summary>
    public static class SwaggerConfig
    {
        /// <summary>
        /// The AddSwaggerGen
        /// </summary>
        /// <param name="swagger">The swagger<see cref="IServiceCollection"/></param>
        public static void AddSwaggerGen(this IServiceCollection swagger)
        {
            swagger.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "MT4 Account Overview API V1.0",
                    Description = "API documentation for version 1"
                });

                c.TagActionsBy(api =>
                {
                    var controllerActionDescriptor = api.ActionDescriptor as ControllerActionDescriptor;
                    if (controllerActionDescriptor != null)
                    {
                        return new[] { controllerActionDescriptor.ControllerTypeInfo.Name.Replace("Controller", "") };
                    }

                    return new[] { "Default" };
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                c.DocInclusionPredicate((_, _) => true);
                c.IncludeXmlComments(xmlPath);

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. " +
                                    "\r\n\r\nEnter 'Bearer' [space] and then your token in the text input below." +
                                    "\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        }, new string[] {}
                    }
                });
            });
        }
    }
}