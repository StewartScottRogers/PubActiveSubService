using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;

namespace PubActiveSubService.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class TraceChannelsV1Controller : ControllerBase {
        private readonly IPubActiveSubServiceProcessors PubActiveSubServiceProcessors;

        public TraceChannelsV1Controller(IPubActiveSubServiceProcessors pubActiveSubServiceProcessors) {
            if (null == pubActiveSubServiceProcessors) throw new ArgumentNullException(nameof(pubActiveSubServiceProcessors));
            PubActiveSubServiceProcessors = pubActiveSubServiceProcessors;
        }

        // POST: api/TraceChannelsV1
        [HttpPost]
        public IEnumerable<Models.TracedChannelV1> Post([FromBody] Models.ChannelSearchV1 channelSearchV1) =>
            PubActiveSubServiceProcessors.Trace(channelSearchV1);
    }
}
