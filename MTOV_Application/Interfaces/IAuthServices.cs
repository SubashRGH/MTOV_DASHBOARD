namespace MTOV_Application.Interfaces
{
    using MTOV_DTO.Auth;

    /// <summary>
    /// Auth interface
    /// </summary>
    public interface IAuthServices
    {
        Task<(AuthResDto?, string)> AuthenticateUser(string userName, string password);
    }
}