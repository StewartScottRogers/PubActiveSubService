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
        public IEnumerable<Models.TracedChannelV1> Post([FromBody] Models.TraceChannelsV1 traceChannelsV1) {
            yield return new Models.TracedChannelV1() { ChannelName = "Channel/One", Subscribers = new Models.SubscriberStatusV1[] { new Models.SubscriberStatusV1() { Subscriber = "SubscriberOne", Status = new Models.StatusV1[] { new Models.StatusV1() { Name = "Connected.", Value = "OK" } } } } };
            yield return new Models.TracedChannelV1() { ChannelName = "Channel/Two" };
            yield return new Models.TracedChannelV1() { ChannelName = "Channel/Three" };
            yield return new Models.TracedChannelV1() { ChannelName = "Channel/Four" };
            yield return new Models.TracedChannelV1() { ChannelName = "Channel/Five" };
            yield return new Models.TracedChannelV1() { ChannelName = "Channel/Six" };
            yield return new Models.TracedChannelV1() { ChannelName = "Channel/Seven" };
            yield return new Models.TracedChannelV1() { ChannelName = "Channel/Eight" };
            yield return new Models.TracedChannelV1() { ChannelName = "Channel/Nine" };
            yield return new Models.TracedChannelV1() { ChannelName = "Channel/Ten" };
        }
    }
}
