namespace PubActiveSubService.Models {
    [System.Serializable]
    public class TracedChannel {
        public string ChannelName { get; set; } = string.Empty;

        public SubscriberStatus[] Subscribers { get; set; } = new SubscriberStatus[] { };
    }
}
