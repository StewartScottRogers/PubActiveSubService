using System.Collections.Generic;

namespace PubActiveSubService.Models {
    [System.Serializable]
    public class ChannelStatus {
        public string ChannelName { get; set; } = string.Empty;

        public List<SubscriberStatus> SubscriberStatuses { get; set; } = new List<SubscriberStatus>();
    }
}
