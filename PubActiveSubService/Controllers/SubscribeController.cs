using Microsoft.AspNetCore.Mvc;
using System;

namespace PubActiveSubService.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribeController : ControllerBase {
        public SubscribeController(IIntegrationProcessors integrationProcessors) : base(integrationProcessors) { }

        [HttpPost]
        public void Post([FromBody] Models.Subscribe subscribe) {
            RecordHostUrl();
            IntegrationProcessors.Subscribe(subscribe);
        }
    }
}
