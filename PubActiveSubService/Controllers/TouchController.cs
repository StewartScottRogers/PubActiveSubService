using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Net.Mime;

namespace PubActiveSubService.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class TouchController : ControllerBase {
        public TouchController(IIntegrationProcessors integrationProcessors) : base(integrationProcessors) { }

        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public string Get() {
            RecordHostUrl();
            return IntegrationProcessors.Touch();
        }
    }
}
