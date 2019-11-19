namespace PubActiveSubService.Models {
    [System.Serializable]
    public class SubscriberStatus {
        public string SubscriberName { get; set; } = string.Empty;
        public Status[] Status { get; set; } = new Status[] { };
    }
}