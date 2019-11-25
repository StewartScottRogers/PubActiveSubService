using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Net.Mime;

namespace PubActiveSubService.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class TouchThroughController : ControllerBase {
        public TouchThroughController(IIntegrationProcessors integrationProcessors) : base(integrationProcessors) { }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Models.Results Post([FromBody] Models.RestUrl restUrl) {
            RecordHostUrl();
            return IntegrationProcessors.TouchThrough(restUrl.Url);
        }
    }
}
