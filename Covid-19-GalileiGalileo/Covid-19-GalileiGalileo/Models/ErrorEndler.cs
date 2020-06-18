using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http;

namespace Covid_World.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }


    public class ErrorAPI:IDisposable
    {
        public HttpResponseMessage HttpResponse;

        public string Description { get; set; }

        public void Dispose()
        {
            HttpResponse?.Dispose();
        }
    }

}
