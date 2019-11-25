using Microsoft.AspNetCore.Mvc;
using System;

namespace PubActiveSubService.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class PublishController : ControllerBase {
        public PublishController(IIntegrationProcessors integrationProcessors) : base(integrationProcessors) { }

        [HttpPost]
        public Models.Package[] Post([FromBody] Models.Package package) {
            RecordHostUrl();
            return IntegrationProcessors.Publish(package);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public string Get() {
            RecordHostUrl();
            return IntegrationProcessors.Touch();
        }
    }
}
