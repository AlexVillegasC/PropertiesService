using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace PropertyService.CloudFoundry
{
    [ExcludeFromCodeCoverage]
    public class VcapAppEnvironment
    {
        [JsonProperty("application_uris")]
        public List<string> ApplicationUris { get; set; }

        [JsonProperty("port")]
        public int Port { get; set; }

        [JsonProperty("space_name")]
        public string Space { get; set; }
    }
}