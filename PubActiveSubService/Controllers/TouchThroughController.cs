using Microsoft.AspNetCore.Mvc;
using System;

namespace PubActiveSubService.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class TouchThroughController : ControllerBase {
        public TouchThroughController(IIntegrationProcessors integrationProcessors) : base(integrationProcessors) { }

        [HttpPost]
        public Models.Results Post([FromBody] Models.TouchThrough touchThrough) {
            RecordHostUrl();
            return IntegrationProcessors.TouchThrough(touchThrough.Url);
        }
    }
}
