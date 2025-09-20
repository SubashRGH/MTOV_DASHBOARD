namespace RECRM_API.Middlewares
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using MTOV_VModel.Common;
    using System.Linq;
    using System.Security.Claims;

    /// <summary>
    /// Defines the <see cref="InternalRequestFilter" />
    /// </summary>
    public class InternalRequestFilter : IActionFilter
    {
        /// <summary>
        /// Defines the _logger
        /// </summary>
        private readonly ILogger<InternalRequestFilter> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="InternalRequestFilter"/> class.
        /// </summary>
        /// <param name="logger">The logger<see cref="ILogger{InternalReqFilter}"/></param>
        /// <param name="appProperty">The authProperty<see cref="AppProperty"/></param>
        /// <param name="authenticationServices">The authenticationServices <see cref="IAuthenticationServices"/></param>
        public InternalRequestFilter(ILogger<InternalRequestFilter> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// The OnActionExecuting
        /// </summary>
        /// <param name="context">The context<see cref="ActionExecutingContext"/></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var httpContext = context.HttpContext;
            var user = httpContext.User;

            // Check if the action or controller has the AllowAnonymous attribute
            var allowAnonymous = context.ActionDescriptor
                                        .EndpointMetadata
                                        .OfType<Microsoft.AspNetCore.Authorization.AllowAnonymousAttribute>()
                                        .Any();

            if (allowAnonymous)
            {
                // If AllowAnonymous, store minimal user details
                var internalReq = new InternalRequest
                {
                    Lang = GetLang(httpContext!),
                    VirtualPath = "./",
                    Message = "Anonymous access"
                };

                httpContext!.Items["InternalReq"] = internalReq;
                _logger.LogInformation($"Anonymous user accessed {httpContext.Request.Path}");
                return;
            }
            else
            {
                // Extract user details from claims
                var identifierID = user.FindFirstValue(ClaimTypes.NameIdentifier);
                var userID = user.FindFirstValue(ClaimTypes.PrimarySid);

                if (string.IsNullOrEmpty(identifierID) || !Guid.TryParse(userID, out Guid sessionId))
                {
                    _logger.LogWarning("Unauthorized access attempt");
                    context.Result = new UnauthorizedObjectResult(new { Message = GlobalConstants.INVALID_SESSION_OR_EXPIRED });
                    return;
                }

                // must check with Redis cache or Readonly Replica DB to make sure the session is valid
                var isValidSession = true;

                if (isValidSession)
                {
                    var authenticatedInternalReq = new InternalRequest
                    {
                        UserID = "GUEST_USER_00001",
                        SessionId = sessionId,
                        Lang = GetLang(httpContext!),
                        VirtualPath = "./",
                        Message = "User authenticated",
                    };

                    // Store in HttpContext.Items for controller use
                    httpContext!.Items["InternalReq"] = authenticatedInternalReq;

                    _logger.LogInformation($"User {authenticatedInternalReq.UserID} - accessed {httpContext.Request.Path}");
                }
                else
                {
                    _logger.LogWarning("Unauthorized access attempt");
                    context.Result = new UnauthorizedObjectResult(new { Message = GlobalConstants.INVALID_SESSION_OR_EXPIRED });
                    return;
                }
            }
        }

        /// <summary>
        /// The GetLang - Getting user default language culture
        /// </summary>
        /// <param name="httpContext">The httpContext<see cref="HttpContext"/></param>
        /// <returns>The <see cref="string"/></returns>
        private string GetLang(HttpContext httpContext)
        {
            return httpContext.Request.Headers["Accept-Language"].ToString() ?? "en";
        }

        /// <summary>
        /// The OnActionExecuted
        /// </summary>
        /// <param name="context">The context<see cref="ActionExecutedContext"/></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}