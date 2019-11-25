using Microsoft.AspNetCore.Mvc;
using System;

namespace PubActiveSubService.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class PublishLoopbackController : ControllerBase {
        public PublishLoopbackController(IIntegrationProcessors integrationProcessors) : base(integrationProcessors) { }

        [HttpPost]
        public Models.Package[] Post([FromBody] Models.Package package) {
            RecordHostUrl();
            return IntegrationProcessors.PublishLoopback(package);
        }

        [HttpGet]
        public string Get() {
            RecordHostUrl();
            return IntegrationProcessors.Touch();
        }
    }
}
