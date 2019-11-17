using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;

namespace PubActiveSubService.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ListChannelsV1Controller : ControllerBase {
        // POST: api/ListChannelsV1
        [HttpPost]
        public IEnumerable<Models.ListedChannelV1> Post([FromBody] Models.ListChannelsV1 listChannelsV1) {
            yield return new Models.ListedChannelV1() { ChannelName = "Channel/One", Subscribers = new string[] { "SubscriberOne", "SubscriberTwo", "SubscriberThree" } };
            yield return new Models.ListedChannelV1() { ChannelName = "Channel/Two", Subscribers = new string[] { "SubscriberOne", "SubscriberTwo", "SubscriberThree" } };
            yield return new Models.ListedChannelV1() { ChannelName = "Channel/Three", Subscribers = new string[] { "SubscriberOne", "SubscriberTwo", "SubscriberThree" } };
            yield return new Models.ListedChannelV1() { ChannelName = "Channel/Four", Subscribers = new string[] { "SubscriberOne", "SubscriberTwo", "SubscriberThree" } };
            yield return new Models.ListedChannelV1() { ChannelName = "Channel/Five", Subscribers = new string[] { "SubscriberOne", "SubscriberTwo", "SubscriberThree" } };
            yield return new Models.ListedChannelV1() { ChannelName = "Channel/Six", Subscribers = new string[] { "SubscriberOne", "SubscriberTwo", "SubscriberThree" } };
            yield return new Models.ListedChannelV1() { ChannelName = "Channel/Seven", Subscribers = new string[] { "SubscriberOne", "SubscriberTwo", "SubscriberThree" } };
            yield return new Models.ListedChannelV1() { ChannelName = "Channel/Eight", Subscribers = new string[] { "SubscriberOne", "SubscriberTwo", "SubscriberThree" } };
            yield return new Models.ListedChannelV1() { ChannelName = "Channel/Nine", Subscribers = new string[] { "SubscriberOne", "SubscriberTwo", "SubscriberThree" } };
            yield return new Models.ListedChannelV1() { ChannelName = "Channel/Ten", Subscribers = new string[] { "SubscriberOne", "SubscriberTwo", "SubscriberThree" } };
        }
    }
}
