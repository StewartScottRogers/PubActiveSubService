namespace PubActiveSubService.Models {
    public class TracedChannelV1 {
        public string ChannelName { get; set; } = string.Empty;

        public SubscriberStatusV1[] Subscribers { get; set; } = new SubscriberStatusV1[] { };
    }
}
