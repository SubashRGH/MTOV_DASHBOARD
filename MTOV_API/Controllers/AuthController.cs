using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MTOV_Application.Interfaces;
using MTOV_DTO.Account;
using MTOV_DTO.Auth;
using MTOV_Utility;
using RECRM_API.Controllers;

namespace MTOV_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(ILogger<AuthController> _logger, IAuthServices _authServices) : BaseController<AuthController>(_logger)
    {
        /// <summary>
        /// Authentication
        /// </summary>
        /// <param name="request"> auth request</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("")]
        [ProducesResponseType<AccountDetailResDto>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Auth([FromBody] AuthReqDto request)
        {
            (AuthResDto result, string message) = await _authServices.AuthenticateUser(request.UserName, request.Password);
            response = ResponseFormatter.FormatSuccessResponse(message, result);
            return Ok(response);
        }
    }
}