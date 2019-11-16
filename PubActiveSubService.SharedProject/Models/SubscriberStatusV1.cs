namespace PubActiveSubService.Models {
    public class SubscriberStatusV1 {
        public string Subscriber { get; set; } = string.Empty;
        public StatusV1[] Status { get; set; } = new StatusV1[] { };
    }
}