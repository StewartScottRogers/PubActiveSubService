using Microsoft.AspNetCore.Mvc;
using System;

namespace PubActiveSubService.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class TouchController : ControllerBase {
        public TouchController(IIntegrationProcessors integrationProcessors) : base(integrationProcessors) { }

        [HttpGet]
        public string Get() {
            RecordHostUrl();
            return IntegrationProcessors.Touch();
        }
    }
}
