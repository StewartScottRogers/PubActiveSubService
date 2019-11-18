namespace PubActiveSubService.Models {
    [System.Serializable]
    public class TracedChannelV1 {
        public string ChannelName { get; set; } = string.Empty;

        public SubscriberStatusV1[] Subscribers { get; set; } = new SubscriberStatusV1[] { };
    }
}
