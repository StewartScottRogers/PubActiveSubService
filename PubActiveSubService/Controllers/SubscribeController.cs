using Microsoft.AspNetCore.Mvc;
using System;

namespace PubActiveSubService.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribeController : ControllerBase {
        private readonly IIntegrationProcessors PubActiveSubServiceProcessors;

        public SubscribeController(IIntegrationProcessors pubActiveSubServiceProcessors) {
            if (null == pubActiveSubServiceProcessors) throw new ArgumentNullException(nameof(pubActiveSubServiceProcessors));
            PubActiveSubServiceProcessors = pubActiveSubServiceProcessors;
        }

        [HttpPost]
        public void Post([FromBody] Models.Subscribe subscribe) {
            PubActiveSubServiceProcessors.SaveHostUrl($"{Request.Scheme}://{Request.Host.Value}");
            PubActiveSubServiceProcessors.Subscribe(subscribe);
        }
    }
}
