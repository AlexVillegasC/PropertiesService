using PropertyService.Impersonation;

namespace PropertyService.Configuration
{
    public class SystemBaseUrlsOptions
    {
        public static ImpersonationOptions Impersonation { get; set; }

        public string APIName { get; set; }

        public string ApiKey { get; set; }

        public string Authorization { get; set; }
    }
}
