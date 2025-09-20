namespace MTOV_DTO.Auth
{
    public class AuthResDto
    {
        /// <summary>
        /// Gets or sets the Token
        /// </summary>
        public string? Token { get; set; }
    }

    public class AuthReqDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}