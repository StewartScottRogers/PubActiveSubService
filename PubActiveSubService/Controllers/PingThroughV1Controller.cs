using Microsoft.AspNetCore.Mvc;

using System;

namespace PubActiveSubService.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class PingThroughV1Controller : ControllerBase {
        private readonly IPubActiveSubServiceProcessors PubActiveSubServiceProcessors;

        public PingThroughV1Controller(IPubActiveSubServiceProcessors pubActiveSubServiceProcessors) {
            if (null == pubActiveSubServiceProcessors) throw new ArgumentNullException(nameof(pubActiveSubServiceProcessors));
            PubActiveSubServiceProcessors = pubActiveSubServiceProcessors;
        }

        [HttpPost]
        public string Post([FromBody] Models.PingThroughV1 pingThroughV1) {
            PubActiveSubServiceProcessors.SaveArchiveHostDns($"{Request.Scheme}://{Request.Host.Value}");
            return PubActiveSubServiceProcessors.Pingthrough(pingThroughV1.Url);
        }
    }
}
