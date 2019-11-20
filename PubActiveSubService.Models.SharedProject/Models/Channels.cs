using System.Collections.Generic;

namespace PubActiveSubService.Models {
    [System.Serializable]
    public class Channels {
        public List<Channel> ChannelList { get; set; } = new List<Channel>();
    }
}
