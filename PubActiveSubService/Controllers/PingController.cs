using Microsoft.AspNetCore.Mvc;
using System;

namespace PubActiveSubService.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class PingController : ControllerBase {
        private readonly IIntegrationProcessors PubActiveSubServiceProcessors;

        public PingController(IIntegrationProcessors pubActiveSubServiceProcessors) {
            if (null == pubActiveSubServiceProcessors) throw new ArgumentNullException(nameof(pubActiveSubServiceProcessors));
            PubActiveSubServiceProcessors = pubActiveSubServiceProcessors;
        }

        [HttpGet]
        public string Get() {
            PubActiveSubServiceProcessors.SaveHostUrl($"{Request.Scheme}://{Request.Host.Value}");
            return PubActiveSubServiceProcessors.Ping();
        }
    }
}
