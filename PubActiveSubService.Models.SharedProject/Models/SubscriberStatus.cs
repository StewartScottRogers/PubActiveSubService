using System.Collections.Generic;

namespace PubActiveSubService.Models {
    [System.Serializable]
    public class SubscriberStatus {
        public string SubscriberName { get; set; } = string.Empty;
        public string RestUrl { get; set; } = string.Empty;
        public List<NameValuePair> Status { get; set; } = new List<NameValuePair>();
    }
}