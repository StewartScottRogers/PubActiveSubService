using System.Collections.Generic;

namespace PubActiveSubService.Models {
    [System.Serializable]
    public class SubscriberStatus {
        public string SubscriberName { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public List<Status> Status { get; set; } = new List<Status>();
    }
}