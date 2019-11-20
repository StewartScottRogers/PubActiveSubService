using Microsoft.AspNetCore.Mvc;
using System;

namespace PubActiveSubService.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UnsubscribeController : ControllerBase {
        public UnsubscribeController(IIntegrationProcessors integrationProcessors) : base(integrationProcessors) { }

        [HttpPost]
        public void Post([FromBody] Models.Unsubscribe unsubscribe) {
            RecordHostUrl();
            IntegrationProcessors.Unsubscribe(unsubscribe);
        }
    }
}
