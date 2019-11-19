namespace PubActiveSubService.Models {
    [System.Serializable]
    public class SubscriberV1 {
        public string SubscriberName { get; set; } = string.Empty;

        public bool Enabled { get; set; } = false;

        public string SubscriberPostUrl { get; set; } = string.Empty;
    }
}