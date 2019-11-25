﻿namespace PubActiveSubService.Models {
    [System.Serializable]
    public class Subscriber {
        public string SubscriberName { get; set; } = string.Empty;

        public bool Enabled { get; set; } = false;

        public string RestUrl { get; set; } = string.Empty;

        public override string ToString() => $"{nameof(SubscriberName)}: '{SubscriberName}', {nameof(Enabled)}: '{Enabled}', {nameof(RestUrl)}: '{RestUrl}'.";
    }
}