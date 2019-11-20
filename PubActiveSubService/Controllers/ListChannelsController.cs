using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace PubActiveSubService.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ListChannelsController : ControllerBase {
        public ListChannelsController(IIntegrationProcessors integrationProcessors):base(integrationProcessors) {}

        [HttpPost]
        public IEnumerable<Models.ListedChannel> Post([FromBody] Models.ChannelSearch channelSearch) {
            RecordHostUrl();
            return IntegrationProcessors.ListChannels(channelSearch);
        }
    }
}
