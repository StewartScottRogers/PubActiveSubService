﻿using System.Collections.Generic;

namespace PubActiveSubService.Models {
    [System.Serializable]
    public class ChannelsV1 {
        public List<ChannelV1> Channels { get; set; } = new List<ChannelV1>();
    }
}