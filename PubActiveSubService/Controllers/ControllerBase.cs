using Microsoft.AspNetCore.Mvc;
using System;

namespace PubActiveSubService.Controllers {
    public class ControllerBase : Controller {
        protected readonly IIntegrationProcessors IntegrationProcessors;
        private static string HostUrl = "";

        public ControllerBase(IIntegrationProcessors integrationProcessors) {
            if (null == integrationProcessors) throw new ArgumentNullException(nameof(integrationProcessors));
            IntegrationProcessors = integrationProcessors;
        }

        protected void RecordHostUrl() {
            if (HostUrl.Length <= 0) {
                HostUrl = $"{Request.Scheme}://{Request.Host.Value}";
                IntegrationProcessors.SaveHostUrl(HostUrl);
            }
        }
    }
}
