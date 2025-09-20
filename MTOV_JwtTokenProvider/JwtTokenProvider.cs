namespace MTOV_JwtTokenProvider
{
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Runtime.Serialization;
    using System.Security.Claims;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.Json.Serialization;
    
    /// <summary>
    /// Defines the <see cref="JWTToken" />
    /// </summary>
    public class JWTToken
    {
        /// <summary>
        /// Gets or sets the AccessToken
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Gets or sets the RefreshToken
        /// </summary>
        public string RefreshToken { get; set; }

        [IgnoreDataMember]
        [JsonIgnore]
        public int ExpirySecond { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="JwtTokenProvider" />
    /// </summary>
    public class JwtTokenProvider : IJwtTokenProvider
    {
        /// <summary>
        /// Defines the _issuer
        /// </summary>
        private readonly string _issuer;

        /// <summary>
        /// Defines the _audience
        /// </summary>
        private readonly string _audience;

        private readonly int _tokenExpiryInSeconds;

        /// <summary>
        /// Defines the _signingKey
        /// </summary>
        private readonly SymmetricSecurityKey _signingKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtTokenProvider"/> class.
        /// </summary>
        /// <param name="issuer">The issuer<see cref="string"/></param>
        /// <param name="audience">The audience<see cref="string"/></param>
        /// <param name="secretKey">The secretKey<see cref="string"/></param>
        public JwtTokenProvider(IOptions<JwtTokenProviderOptionsBuilder> options)
        {
            _issuer = options.Value.Issuer;
            _audience = options.Value.Audience;
            int.TryParse(options.Value.TimeOut, out _tokenExpiryInSeconds);
            _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(options.Value.SecretKey));
        }

        /// <summary>
        /// The GetToken
        /// </summary>
        /// <param name="preDefainedClaims">The preDefainedClaims<see cref="List{Claim}"/></param>
        /// <param name="customClaims">The customClaims<see cref="Dictionary{string, string}?"/></param>
        /// <param name="tokenExpiryInSeconds">The tokenExpiryInSeconds<see cref="int"/></param>
        /// <returns>The <see cref="JWTToken"/></returns>
        public JWTToken GetToken(List<Claim> preDefainedClaims, Dictionary<string, string>? customClaims = null, int tokenExpiryInSeconds = 1000)
        {
            if (tokenExpiryInSeconds == 60)
            {
                tokenExpiryInSeconds = _tokenExpiryInSeconds;
            }
            DateTime value = DateTime.UtcNow.AddSeconds(tokenExpiryInSeconds);
            if (customClaims != null && customClaims.Count > 0)
            {
                foreach (KeyValuePair<string, string> customClaim in customClaims)
                {
                    preDefainedClaims.Add(new Claim(customClaim.Key, customClaim.Value));
                }
            }

            string issuer = _issuer;
            string audience = _audience;
            DateTime? expires = value;
            SigningCredentials signingCredentials = new SigningCredentials(_signingKey, "HS256");
            JwtSecurityToken token = new JwtSecurityToken(issuer, audience, preDefainedClaims, null, expires, signingCredentials);
            return new JWTToken
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = GenerateRefreshToken(),
                ExpirySecond = tokenExpiryInSeconds
            };
        }

        /// <summary>
        /// The RefreshToken
        /// </summary>
        /// <param name="request">The request<see cref="JWTToken"/></param>
        /// <param name="tokenExpiryInSeconds">The tokenExpiryInSeconds<see cref="int"/></param>
        /// <returns>The <see cref="(JWTToken?, string)"/></returns>
        public (JWTToken?, string) RefreshToken(JWTToken request, int tokenExpiryInSeconds = 60)
        {
            string errMsg = string.Empty;
            ClaimsPrincipal principalFromExpiredToken = GetPrincipalFromExpiredToken(request.AccessToken!, out errMsg)!;
            if (principalFromExpiredToken == null)
            {
                return (null, "Invalid access token");
            }
            if (tokenExpiryInSeconds == 60)
            {
                tokenExpiryInSeconds = _tokenExpiryInSeconds;
            }
            DateTime value = DateTime.UtcNow.AddMinutes(tokenExpiryInSeconds);
            string issuer = _issuer;
            string audience = _audience;
            DateTime? expires = value;
            IEnumerable<Claim> claims = principalFromExpiredToken.Claims;
            SigningCredentials signingCredentials = new SigningCredentials(_signingKey, "HS256");
            JwtSecurityToken token = new JwtSecurityToken(issuer, audience, claims, null, expires, signingCredentials);
            string accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            string refreshToken = GenerateRefreshToken();
            return (new JWTToken
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            }, string.Empty);
        }

        /// <summary>
        /// The GetPrincipalFromExpiredToken
        /// </summary>
        /// <param name="token">The token<see cref="string?"/></param>
        /// <param name="errMsg">The errMsg<see cref="string"/></param>
        /// <returns>The <see cref="ClaimsPrincipal?"/></returns>
        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token, out string errMsg)
        {
            errMsg = string.Empty;
            TokenValidationParameters validationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = _signingKey,
                ValidAudience = _audience,
                ValidIssuer = _issuer,
                ValidateLifetime = true,
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = false,
                ClockSkew = TimeSpan.FromSeconds(0L)
            };
            SecurityToken validatedToken;
            ClaimsPrincipal result = new JwtSecurityTokenHandler().ValidateToken(token, validationParameters, out validatedToken);
            if (!(validatedToken is JwtSecurityToken jwtSecurityToken) || !jwtSecurityToken.Header.Alg.Equals("HS256", StringComparison.InvariantCultureIgnoreCase))
            {
                errMsg = "Invalid token";
            }

            return result;
        }

        /// <summary>
        /// The GenerateRefreshToken
        /// </summary>
        /// <returns>The <see cref="string"/></returns>
        private static string GenerateRefreshToken()
        {
            byte[] array = new byte[64];
            using RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(array);
            return Convert.ToBase64String(array);
        }

        /// <summary>
        /// The GetValidationParameters
        /// </summary>
        /// <returns>The <see cref="TokenValidationParameters"/></returns>
        public TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters
            {
                IssuerSigningKey = _signingKey,
                ValidAudience = _audience,
                ValidIssuer = _issuer,
                ValidateLifetime = true,
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = false,
                ClockSkew = TimeSpan.FromSeconds(0L)
            };
        }
    }
}