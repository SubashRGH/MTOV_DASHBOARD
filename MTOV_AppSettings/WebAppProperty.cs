namespace MTOV_AppSettings
{
    using AppSettings_Reader;

    public class WebAppProperty
    {
        private readonly Helper helper;

        public WebAppProperty()
        {
            helper = new Helper();

            ApiConfigProp ??= helper.GetSettingsBySection<APIConfigProperty>("APIConfig");
        }


        //// AWS or Azure key valult must be integration for secured key management

        public APIConfigProperty? ApiConfigProp { get; set; }
    }

    public class APIConfigProperty
    {
        public string BaseUrl { get; set; }
        public int TimeOut { get; set; }
        public AuthProperty Auth { get; set; }
        public Account Account { get; set; }
    }

    public class AuthProperty
    {
        public string Login { get; set; }
        public string AuthUser { get; set; }
        public string AuthPass { get; set; }
    }

    public class Account
    {
        public string AccountDetail { get; set; }
        public string OpenTrades { get; set; }
    }
}