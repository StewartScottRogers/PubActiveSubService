using System.Collections.Generic;

namespace PubActiveSubService.Models {
    [System.Serializable]
    public class PublishPackage {
        public List<NameValuePair> Attributes = new List<NameValuePair>();
        public string ChannelName { get; set; } = string.Empty;
        public string Package { get; set; } = string.Empty;
    }
}
