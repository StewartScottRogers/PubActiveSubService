using System.Collections.Generic;

namespace PubActiveSubService.Models {
    [System.Serializable]
    public class Package {
        public string ChannelName { get; set; } = string.Empty;


        public List<NameValuePair> MessageHeaders = new List<NameValuePair>();

        public string Message { get; set; } = string.Empty;
    }
}
