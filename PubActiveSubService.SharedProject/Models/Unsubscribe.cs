namespace PubActiveSubService.Models {
    [System.Serializable]
    public class Unsubscribe {
        public string ChannelName { get; set; } = string.Empty;
        public string SubscriberName { get; set; } = string.Empty;
    }
}