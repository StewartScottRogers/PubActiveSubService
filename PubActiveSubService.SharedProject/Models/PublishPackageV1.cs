namespace PubActiveSubService.Models {
    [System.Serializable]
    public class PublishPackageV1 {
        public string Channel { get; set; } = string.Empty;
        public string Package { get; set; } = string.Empty;
    }
}
