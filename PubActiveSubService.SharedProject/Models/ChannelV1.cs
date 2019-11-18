using System.Collections.Generic;

namespace PubActiveSubService.Models {
    public class ChannelV1 {
        public string Name { get; set; } = string.Empty;

        public List<SubscribeV1> Subscribes { get; set; } = new List<SubscribeV1>();
    }
}
