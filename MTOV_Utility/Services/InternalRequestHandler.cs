namespace MTOV_Utility.Services
{
    using Microsoft.AspNetCore.Http;
    using MTOV_Utility.Interfaces;
    using MTOV_VModel.Common;

    /// <summary>
    /// Defines the <see cref="InternalRequestHandler" />
    /// </summary>
    public class InternalRequestHandler : IInternalRequestHandler
    {
        /// <summary>
        /// Defines the _httpContextAccessor
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="RECRM_VModel.Common.InternalRequest"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">The httpContextAccessor<see cref="IHttpContextAccessor"/></param>
        public InternalRequestHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// The InternalRequest
        /// </summary>
        /// <returns>The <see cref="RECRM_VModel.Common.InternalRequest"/></returns>
        public InternalRequest InternalRequest
        {
            get
            {
                if (_httpContextAccessor.HttpContext!.Items.TryGetValue("InternalReq", out var internalReqObj) && internalReqObj is InternalRequest internalReq)
                {
                    return internalReq!;
                }
                return null!;
            }
        }
    }
}