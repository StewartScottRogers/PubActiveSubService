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
        public IEnumerable<Models.ListedChannelV1> Post([FromBody] Models.ListChannelsV1 listChannelsV1)
            => PubActiveSubServiceProcessors.ListChannels(listChannelsV1);
    }
}
