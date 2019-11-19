namespace PubActiveSubService.Models {
    [System.Serializable]
    public class SubscriberStatusV1 {
        public string SubscriberName { get; set; } = string.Empty;
        public StatusV1[] Status { get; set; } = new StatusV1[] { };
    }
}