using System.Collections.Generic;

namespace PubActiveSubService.Models {
    [System.Serializable]
    public class ListedChannelV1 {
        public string ChannelName { get; set; } = string.Empty;

        public List<SubscriberV1> Subscribers { get; set; } = new List<SubscriberV1>();
    }
}
