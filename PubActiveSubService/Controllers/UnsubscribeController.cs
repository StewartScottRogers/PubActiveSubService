using Microsoft.AspNetCore.Mvc;
using System;

namespace PubActiveSubService.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UnsubscribeController : ControllerBase {
        private readonly IIntegrationProcessors PubActiveSubServiceProcessors;

        public UnsubscribeController(IIntegrationProcessors pubActiveSubServiceProcessors) {
            if (null == pubActiveSubServiceProcessors) throw new ArgumentNullException(nameof(pubActiveSubServiceProcessors));
            PubActiveSubServiceProcessors = pubActiveSubServiceProcessors;
        }

        [HttpPost]
        public void Post([FromBody] Models.Unsubscribe unsubscribe) {
            PubActiveSubServiceProcessors.SaveHostUrl($"{Request.Scheme}://{Request.Host.Value}");
            PubActiveSubServiceProcessors.Unsubscribe(unsubscribe);
        }
    }
}
