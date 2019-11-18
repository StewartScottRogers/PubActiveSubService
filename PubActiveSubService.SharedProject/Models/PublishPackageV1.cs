namespace PubActiveSubService.Models {
    [System.Serializable]
    public class PublishPackageV1 {
        public string ChannelName { get; set; } = string.Empty;
        public string Package { get; set; } = string.Empty;
    }
}
