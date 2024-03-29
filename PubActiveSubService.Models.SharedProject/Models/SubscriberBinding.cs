﻿namespace PubActiveSubService.Models {
    [System.Serializable]
    public class SubscriberBinding {
        public string ChannelName { get; set; } = string.Empty;
        public string SubscriberName { get; set; } = string.Empty;

        public override string ToString() => $"{nameof(SubscriberName)}: '{SubscriberName}', {nameof(ChannelName)}: '{ChannelName}'.";
    }
}