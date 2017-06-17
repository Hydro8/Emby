using System.Collections.Generic;
using System.Text;
using MediaBrowser.Common.Net;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Notifications;
using MediaBrowser.Model.Logging;
using Constellation.Configuration;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Constellation
{
    public class Notifier : INotificationService
    {
        private readonly ILogger _logger;
        private readonly IHttpClient _httpClient;

        public Notifier(ILogManager logManager, IHttpClient httpClient)
        {
            _logger = logManager.GetLogger(GetType().Name);
            _httpClient = httpClient;
        }

        public bool IsEnabledForUser(User user)
        {
            var options = GetOptions(user);

            return options != null && IsValid(options) && options.Enabled;
        }

        private ConstellationOptions GetOptions(User user)
        {
            return Plugin.Instance.Configuration.Options
                .FirstOrDefault(i => string.Equals(i.MediaBrowserUserId, user.Id.ToString("N"), StringComparison.OrdinalIgnoreCase));
        }

        public string Name
        {
            get { return Plugin.Instance.Name; }
        }

        public Task SendNotification(UserNotification request, CancellationToken cancellationToken)
        {
            var options = GetOptions(request.User);

            var parameters = new Dictionary<string, string>
                {
                   // {"device_iden", options.DeviceId},
                    {"type", "note"},
                    {"title", request.Name},
                    {"body", request.Description}
                };

            _logger.Debug("PushBullet to Token : {0} - {1} - {2}", options.Credential, options.DeviceId, request.Description);
            var _httpRequest = new HttpRequestOptions();
            string authInfo = options.Credential;

            _httpRequest.Url = String.Format("{0}:{1}/rest/constellation/WriteLog?SentinelName={2}&PackageName={3}&AccessKey={4}&message=Il y  a une erreur ici&level=Error", options.Url, options.Port, options.Sentinel, options.Package, options.Credential);

            return _httpClient.Get(_httpRequest);
        }

        private bool IsValid(ConstellationOptions options)
        {
            return !string.IsNullOrEmpty(options.Credential);
        }
    }
}
