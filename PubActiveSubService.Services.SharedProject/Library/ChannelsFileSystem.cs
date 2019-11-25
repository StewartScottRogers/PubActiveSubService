using System.Collections.Generic;

namespace PubActiveSubService.Library {
    [System.Serializable]
    public class ChannelsFileSystem {
        public List<Models.Channel> Channels { get; set; } = new List<Models.Channel>();
    }
}
