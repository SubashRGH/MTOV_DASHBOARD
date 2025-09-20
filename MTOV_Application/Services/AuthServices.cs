namespace MTOV_Application.Services
{
    using MTOV_DTO.Auth;
    using MTOV_Application.Interfaces;
    using MTOV_AppSettings;
    using MTOV_JwtTokenProvider;
    using RECRM_Application.Constants;
    using System.Security.Claims;

    /// <summary>
    /// Auth services implementing JWT and Interface
    /// </summary>
    /// <param name="_jwtTokenProvider"></param>
    /// <param name="_appProperty"></param>
    public class AuthServices(IJwtTokenProvider _jwtTokenProvider, AppProperty _appProperty) : IAuthServices
    {
        public async Task<(AuthResDto?, string)> AuthenticateUser(string userName, string password)
        {
            AuthResDto? authResult = null;
            string message = string.Empty;

            if (_appProperty.AuthUserName == userName && _appProperty.AuthPass == password)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, userName),
                    new Claim(ClaimTypes.PrimarySid, Guid.NewGuid().ToString()),
                    new (ClaimTypes.Role, "ADMIN001"),
                };

                var token = _jwtTokenProvider.GetToken(claims);

                if (string.IsNullOrEmpty(token?.AccessToken))
                {
                    message = MessagesConstant.TOKEN_GEN_FAILED;
                }
                else
                {
                    authResult = new AuthResDto
                    {
                        Token = token.AccessToken
                    };
                }
            }
            else
            {
                message = MessagesConstant.INVALID_USERNAME_PASS;
            }

            return (authResult, message);
        }
    }
}