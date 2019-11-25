using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace PubActiveSubService.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class TraceChannelsController : ControllerBase {
        public TraceChannelsController(IIntegrationProcessors integrationProcessors) : base(integrationProcessors) { }

        [HttpPost]
        public IEnumerable<Models.ChannelStatus> Post([FromBody] Models.ChannelSearch channelSearch) {
            RecordHostUrl();
            return IntegrationProcessors.TraceChannels(channelSearch);
        }
    }
}
