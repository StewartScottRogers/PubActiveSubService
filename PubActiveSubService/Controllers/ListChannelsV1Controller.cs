using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;

namespace PubActiveSubService.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ListChannelsV1Controller : ControllerBase {
        private readonly IPubActiveSubServiceProcessors PubActiveSubServiceProcessors;

        public ListChannelsV1Controller(IPubActiveSubServiceProcessors pubActiveSubServiceProcessors) {
            if (null == pubActiveSubServiceProcessors) throw new ArgumentNullException(nameof(pubActiveSubServiceProcessors));
            PubActiveSubServiceProcessors = pubActiveSubServiceProcessors;
        }

        [HttpPost]
        public IEnumerable<Models.ListedChannelV1> Post([FromBody] Models.ChannelSearchV1 channelSearchV1) {
            PubActiveSubServiceProcessors.SaveArchiveHostDns($"{Request.Scheme}://{Request.Host.Value}");
            return PubActiveSubServiceProcessors.ListChannels(channelSearchV1);
        }
    }
}
