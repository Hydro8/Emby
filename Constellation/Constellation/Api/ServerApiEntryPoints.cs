using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using MediaBrowser.Common.Net;
using MediaBrowser.Controller.Net;
using MediaBrowser.Model.Logging;
using MediaBrowser.Model.Services;
using Constellation.Configuration;

namespace Constellation.Api
{
    [Route("/Notification/Constellation/Test/{UserID}", "POST", Summary = "Tests Constellation")]
    public class TestNotification : IReturnVoid
    {
        [ApiMember(Name = "UserID", Description = "User Id", IsRequired = true, DataType = "string", ParameterType = "path", Verb = "GET")]
        public string UserID { get; set; }
    }

    class ServerApiEndpoints : IService
    {
        private readonly IHttpClient _httpClient;
        private readonly ILogger _logger;

        public ServerApiEndpoints(ILogManager logManager, IHttpClient httpClient)
        {
            _logger = logManager.GetLogger(GetType().Name);
            _httpClient = httpClient;
        }
        private ConstellationOptions GetOptions(String userID)
        {
            return Plugin.Instance.Configuration.Options
                .FirstOrDefault(i => string.Equals(i.MediaBrowserUserId, userID, StringComparison.OrdinalIgnoreCase));
        }

        public object Post(TestNotification request)
        {
            var options = GetOptions(request.UserID);

            var parameters = new Dictionary<string, string>
            {
                {"type", "note"},
                {"title", "Test Notification" },
                {"body", "This is a test notification from MediaBrowser"}
            };

            // var _httpRequest = new HttpRequestOptions();

            //Create Basic HTTP Auth Header...

            //string authInfo = options.Token;
            // authInfo = Convert.ToBase64String(Encoding.Ge);

            //_httpRequest.RequestHeaders["Authorization"] = "Basic " + authInfo;

            //_httpRequest.Url = "https://api.pushbullet.com/v2/pushes";

            //return _httpClient.Post(_httpRequest, parameters);
            return _httpClient.Post(new HttpRequestOptions { Url = "https://api.pushover.net/1/messages.json" }, parameters);
        }
    }
}
