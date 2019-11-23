using System.Collections.Generic;

namespace PubActiveSubService.Internals.Services.Library {
    public class ChannelMemmory  {
        public string ChannelName { get; set; } = string.Empty;

        public List<SubscriberQueue> Subscribers { get; set; } = new List<SubscriberQueue>();
    }
}
