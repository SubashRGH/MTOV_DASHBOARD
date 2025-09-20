namespace MTOV_JwtTokenProvider
{
    using Microsoft.IdentityModel.Tokens;
    using System.Security.Claims;

    /// <summary>
    /// Defines the <see cref="IJwtTokenProvider" />
    /// </summary>
    public interface IJwtTokenProvider
    {
        /// <summary>
        /// The GetToken
        /// </summary>
        /// <param name="preDefainedClaims">The preDefainedClaims<see cref="List{Claim}"/></param>
        /// <param name="customClaims">The customClaims<see cref="Dictionary{string, string}?"/></param>
        /// <param name="tokenExpiryInSeconds">The tokenExpiryInSeconds<see cref="int"/></param>
        /// <returns>The <see cref="JWTToken"/></returns>
        public JWTToken GetToken(List<Claim> preDefainedClaims, Dictionary<string, string>? customClaims = null, int tokenExpiryInSeconds = 60);

        /// <summary>
        /// The RefreshToken
        /// </summary>
        /// <param name="request">The request<see cref="JWTToken"/></param>
        /// <param name="tokenExpiryInSeconds">The tokenExpiryInSeconds<see cref="int"/></param>
        /// <returns>The <see cref="(JWTToken?, string)"/></returns>
        public (JWTToken?, string) RefreshToken(JWTToken request, int tokenExpiryInSeconds = 60);

        /// <summary>
        /// The GetValidationParameters
        /// </summary>
        /// <returns>The <see cref="TokenValidationParameters"/></returns>
        TokenValidationParameters GetValidationParameters();
    }
}
