using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Newtonsoft.Json;

namespace PropertyService.CloudFoundry
{
    [ExcludeFromCodeCoverage]
    public static class CfAppEnvironment
    {
        private static readonly VcapAppEnvironment VcapAppEnvironment;

        static CfAppEnvironment()
        {
            VcapAppEnvironment = GetVcapApplicationEnv();
        }

        public static bool IsEnabled { get; private set; }

        public static string[] ApplicationUrls =>
            VcapAppEnvironment.ApplicationUris.Select(s => $"http://{s}").ToArray();

        public static int Port => VcapAppEnvironment.Port;

        public static string Space => VcapAppEnvironment.Space;

        private static VcapAppEnvironment GetVcapApplicationEnv()
        {
            var vcapAppString = Environment.GetEnvironmentVariable("VCAP_APPLICATION");
            VcapAppEnvironment vcapApplicationEv = null;
            if (vcapAppString != null)
            {
                vcapApplicationEv = JsonConvert.DeserializeObject<VcapAppEnvironment>(vcapAppString);
                IsEnabled = true;
            }

            return vcapApplicationEv;
        }
    }
}