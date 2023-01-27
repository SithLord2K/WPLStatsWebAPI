using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;


namespace XWPLStatsWebAPI.MessageHandlers
{
    public class APIKeyMessageHandler : DelegatingHandler
    {
        private const string APIKeyToCheck = "TDLoRo8deL0Bd9p6HfFMNONvtWAlz76YFXy3HIKMkgbSTA3Gkhllrle1a5FPiTkUjAuHcSicguMOQMUO7OuGj6nJg5h3VXc8h5gBrx2YRftwc7NRGl2R4cqv22aRJPnB";
        private const string OverrideMethod = "X-HTTP-Method-Override";
        readonly string[] _methods = { "GET" };
        

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage();
            if(request.Method == HttpMethod.Get && request.Headers.Contains(OverrideMethod))
            {
                var method = request.Headers.GetValues(OverrideMethod).FirstOrDefault();
                if(_methods.Contains(method, StringComparer.InvariantCultureIgnoreCase))
                {
                request.Method = new HttpMethod(method);
                }
                response = await base.SendAsync(request, cancellationToken);
                return response;
            }
            bool validKey = false;
            IEnumerable<string> requestHeaders;
            var checkAPIKeyExists = request.Headers.TryGetValues("APIKey", out requestHeaders);
            if(checkAPIKeyExists)
            {
                if(requestHeaders.FirstOrDefault().Equals(APIKeyToCheck))
                {
                    validKey = true;
                }
            }
            if(!validKey)
            {
                return request.CreateResponse(HttpStatusCode.Forbidden, "Invalid API Key");
            }
            response = await base.SendAsync(request, cancellationToken);
            return response;
        }
    }
}