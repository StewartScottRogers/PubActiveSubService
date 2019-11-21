using Microsoft.AspNetCore.Mvc;
using System;

namespace PubActiveSubService.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class PublishController : ControllerBase {
        public PublishController(IIntegrationProcessors integrationProcessors) : base(integrationProcessors) { }

        [HttpPost]
        public Models.PublishPackage[] Post([FromBody] Models.PublishPackage publishPackage) {
            RecordHostUrl();
            return IntegrationProcessors.Publish(publishPackage);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public string Get() {
            RecordHostUrl();
            return IntegrationProcessors.Touch();
        }
    }
}
