using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Net.Mime;

namespace PubActiveSubService.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribeController : ControllerBase {
        public SubscribeController(IIntegrationProcessors integrationProcessors) : base(integrationProcessors) { }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public void Post([FromBody] Models.Subscribe subscribe) {
            RecordHostUrl();
            IntegrationProcessors.Subscribe(subscribe);
        }
    }
}
