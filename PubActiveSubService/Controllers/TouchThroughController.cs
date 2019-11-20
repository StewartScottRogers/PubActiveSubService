using Microsoft.AspNetCore.Mvc;
using System;

namespace PubActiveSubService.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class TouchThroughController : ControllerBase {
        public TouchThroughController(IIntegrationProcessors integrationProcessors) : base(integrationProcessors) { }

        [HttpPost]
        public string Post([FromBody] Models.PingThrough pingThrough) {
            RecordHostUrl();
            return IntegrationProcessors.TouchThrough(pingThrough.Url);
        }
    }
}
