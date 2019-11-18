﻿using System.Collections.Generic;

namespace PubActiveSubService.Models {
    [System.Serializable]
    public class ChannelV1 {
        public string ChannelName { get; set; } = string.Empty;

        public List<SubscribeV1> Subscribes { get; set; } = new List<SubscribeV1>();
    }
}
