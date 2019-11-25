using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Net.Mime;

namespace PubActiveSubService.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ListChannelsController : ControllerBase {
        public ListChannelsController(IIntegrationProcessors integrationProcessors) : base(integrationProcessors) { }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IEnumerable<Models.Channel> Post([FromBody] Models.Search search) {
            RecordHostUrl();
            return IntegrationProcessors.ListChannels(search);
        }
    }
}
