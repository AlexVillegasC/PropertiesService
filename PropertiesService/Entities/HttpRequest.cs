using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropertyService.Entities
{
    public class HttpRequest<T>
    {
        public HttpRequest(T payLoad, string url)
        {
            PayLoad = payLoad;
            TargetUrl = url;
            RequestHeaders = new Dictionary<string, string>();
        }

        public T PayLoad { get; set; }

        public string TargetUrl { get; set; }

        public Dictionary<string, string> RequestHeaders { get; set; }
    }
}
