using System.Collections.Generic;

namespace PubActiveSubService.Models {
    [System.Serializable]
    public class ListedChannel {
        public string ChannelName { get; set; } = string.Empty;

        public List<Subscriber> Subscribers { get; set; } = new List<Subscriber>();
    }
}
