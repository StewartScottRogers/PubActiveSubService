namespace PubActiveSubService.Models {
    [System.Serializable]
    public class SubscribeV1 {
        public string Subscriber { get; set; } = string.Empty;

        public bool Enabled { get; set; } = false;

        public string SubscriberPostUrl { get; set; } = string.Empty;
    }
}