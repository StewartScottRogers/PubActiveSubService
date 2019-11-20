using System.Collections.Generic;

namespace PubActiveSubService.Models {
    [System.Serializable]
    public class PublishPackage {
        public string ChannelName { get; set; } = string.Empty;


        public List<NameValuePair> PackageHeaders = new List<NameValuePair>();
        public string Package { get; set; } = string.Empty;
    }
}
