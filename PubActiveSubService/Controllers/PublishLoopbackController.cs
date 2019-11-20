using Microsoft.AspNetCore.Mvc;
using System;

namespace PubActiveSubService.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class PublishLoopbackController : ControllerBase {
        public PublishLoopbackController(IIntegrationProcessors integrationProcessors) : base(integrationProcessors) { }

        [HttpPost]
        public string Post([FromBody] Models.PublishPackage publishPackage) {
            RecordHostUrl();
            return IntegrationProcessors.PublishLoopback(publishPackage);
        }

        [HttpGet]
        public string Get() {
            RecordHostUrl();
            return IntegrationProcessors.Touch();
        }
    }
}
