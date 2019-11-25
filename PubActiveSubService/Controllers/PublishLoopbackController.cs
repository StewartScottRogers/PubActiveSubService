using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Net.Mime;

namespace PubActiveSubService.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class PublishLoopbackController : ControllerBase {
        public PublishLoopbackController(IIntegrationProcessors integrationProcessors) : base(integrationProcessors) { }

        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
            return IntegrationProcessors.PublishLoopback(package);
        }
    }
}
