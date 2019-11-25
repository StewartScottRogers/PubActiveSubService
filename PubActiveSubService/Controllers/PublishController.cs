using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Net.Mime;

namespace PubActiveSubService.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class PublishController : ControllerBase {
        public PublishController(IIntegrationProcessors integrationProcessors) : base(integrationProcessors) { }

        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ApiExplorerSettings(IgnoreApi = true)]
        public string Get() {
            RecordHostUrl();
            return IntegrationProcessors.Touch();
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Models.Package[] Post([FromBody] Models.Package package) {
            RecordHostUrl();
            return IntegrationProcessors.Publish(package);
        }
    }
}
