using Microsoft.AspNetCore.Mvc;
using System;

namespace PubActiveSubService.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UnsubscribeV1Controller : ControllerBase {
        private readonly IPubActiveSubServiceProcessors PubActiveSubServiceProcessors;

        public UnsubscribeV1Controller(IPubActiveSubServiceProcessors pubActiveSubServiceProcessors) {
            if (null == pubActiveSubServiceProcessors) throw new ArgumentNullException(nameof(pubActiveSubServiceProcessors));
            PubActiveSubServiceProcessors = pubActiveSubServiceProcessors;
        }

        // POST: api/Unsubscribe
        [HttpPost]
        public void Post([FromBody] Models.UnsubscribeV1 unsubscribeV1) {
            PubActiveSubServiceProcessors.SaveArchiveHostDns($"{Request.Scheme}://{Request.Host.Value}");
            PubActiveSubServiceProcessors.Unsubscribe(unsubscribeV1);
        }
    }
}
