namespace RECRM_API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MTOV_VModel.Common;

    /// <summary>
    /// Defines the <see cref="BaseController{T}" />
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<T> : ControllerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController{T}"/> class.
        /// </summary>
        /// <param name="logger">The logger<see cref="ILogger{T}"/></param>
        public BaseController(ILogger<T> logger)
        {
        }

        /// <summary>
        /// Defines the response
        /// </summary>
        public Result? response;

        /// <summary>
        /// Gets the _internalReq
        /// </summary>
        protected InternalRequest? _internalReq => HttpContext.Items["InternalReq"] as InternalRequest;
    }
}