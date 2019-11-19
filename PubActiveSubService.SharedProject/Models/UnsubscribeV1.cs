namespace PubActiveSubService.Models {
    [System.Serializable]
    public class UnsubscribeV1 {
        public string ChannelName { get; set; } = string.Empty;
        public string SubscriberName { get; set; } = string.Empty;
    }
}