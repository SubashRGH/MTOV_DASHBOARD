namespace MTOV_JwtTokenProvider
{
    /// <summary>
    /// Defines the <see cref="JwtTokenProviderOptionsBuilder" />
    /// </summary>
    public class JwtTokenProviderOptionsBuilder
    {
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string SecretKey { get; set; } = string.Empty;
        public string TimeOut { get; set; } = string.Empty;
    }
}
