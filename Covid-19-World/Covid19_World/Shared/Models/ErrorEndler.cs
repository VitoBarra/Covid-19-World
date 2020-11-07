using System;
using System.Net.Http;

namespace Covid19_World.Shared.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }


    public class ErrorAPI
    {
        public HttpResponseMessage httpResponse;



        public string Description { get; set; }
        public HttpResponseMessage HttpResponse { get => httpResponse; set => httpResponse = value; }



    }

}
