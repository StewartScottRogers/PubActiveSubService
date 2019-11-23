namespace PubActiveSubService.Models {
    [System.Serializable]
    public class Subscriber {
        public string SubscriberName { get; set; } = string.Empty;

        public bool Enabled { get; set; } = false;

        public string SubscriberPostUrl { get; set; } = string.Empty;

        public override string ToString() => $"{nameof(SubscriberName)}: '{SubscriberName}', {nameof(Enabled)}: '{Enabled}', {nameof(SubscriberPostUrl)}: '{SubscriberPostUrl}'.";
    }
}