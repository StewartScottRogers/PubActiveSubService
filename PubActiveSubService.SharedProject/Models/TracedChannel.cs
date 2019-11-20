using System.Collections.Generic;

namespace PubActiveSubService.Models {
    [System.Serializable]
    public class TracedChannel {
        public string ChannelName { get; set; } = string.Empty;

        public List<SubscriberStatus> Subscribers { get; set; } = new List<SubscriberStatus>();
    }
}
