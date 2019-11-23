using System.Collections.Generic;

namespace PubActiveSubService.Models {
    [System.Serializable]
    public class Channel {
        public string ChannelName { get; set; } = string.Empty;

        public List<Subscriber> Subscribers { get; set; } = new List<Subscriber>();

        public override string ToString() => $"{nameof(ChannelName)}: '{ChannelName}'.";
    }
}
