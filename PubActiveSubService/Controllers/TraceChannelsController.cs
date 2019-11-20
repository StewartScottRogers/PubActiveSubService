using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace PubActiveSubService.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class TraceChannelsController : ControllerBase {
        private readonly IIntegrationProcessors PubActiveSubServiceProcessors;

        public TraceChannelsController(IIntegrationProcessors pubActiveSubServiceProcessors) {
            if (null == pubActiveSubServiceProcessors) throw new ArgumentNullException(nameof(pubActiveSubServiceProcessors));
            PubActiveSubServiceProcessors = pubActiveSubServiceProcessors;
        }

        [HttpPost]
        public IEnumerable<Models.TracedChannel> Post([FromBody] Models.ChannelSearch channelSearch) =>
            PubActiveSubServiceProcessors.TraceChannels(channelSearch);
    }
}
