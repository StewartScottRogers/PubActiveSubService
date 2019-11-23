namespace PubActiveSubService.Models {
    [System.Serializable]
    public class Subscribe {
        public string ChannelName { get; set; } = string.Empty;

        public string SubscriberName { get; set; } = string.Empty;

        public bool Enabled { get; set; } = false;

        public string SubscriberPostUrl { get; set; } = string.Empty;

        public override string ToString() => $"{nameof(SubscriberName)}: '{SubscriberName}', {nameof(ChannelName)}: '{ChannelName}', {nameof(Enabled)}: {Enabled}, {nameof(SubscriberPostUrl)}: '{SubscriberPostUrl}'.";
    }
}