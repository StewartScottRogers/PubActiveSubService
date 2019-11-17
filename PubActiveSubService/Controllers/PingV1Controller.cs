using Microsoft.AspNetCore.Mvc;

using System;

namespace PubActiveSubService.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class PingV1Controller : ControllerBase {
        private readonly IPubActiveSubServiceProcessors PubActiveSubServiceProcessors;

        public PingV1Controller(IPubActiveSubServiceProcessors pubActiveSubServiceProcessors) {
            if (null == pubActiveSubServiceProcessors) throw new ArgumentNullException(nameof(pubActiveSubServiceProcessors));
            PubActiveSubServiceProcessors = pubActiveSubServiceProcessors;
        }

        // GET: api/Ping
        [HttpGet]
        public string Get() =>
             DateTimeOffset.UtcNow.ToString();
    }
}
