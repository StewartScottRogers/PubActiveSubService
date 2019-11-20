using Microsoft.AspNetCore.Mvc;
using System;

namespace PubActiveSubService.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class TouchThroughController : ControllerBase {
        private readonly IIntegrationProcessors PubActiveSubServiceProcessors;

        public TouchThroughController(IIntegrationProcessors pubActiveSubServiceProcessors) {
            if (null == pubActiveSubServiceProcessors) throw new ArgumentNullException(nameof(pubActiveSubServiceProcessors));
            PubActiveSubServiceProcessors = pubActiveSubServiceProcessors;
        }

        [HttpPost]
        public string Post([FromBody] Models.PingThrough pingThrough) {
            PubActiveSubServiceProcessors.SaveHostUrl($"{Request.Scheme}://{Request.Host.Value}");
            return PubActiveSubServiceProcessors.TouchThrough(pingThrough.Url);
        }
    }
}
