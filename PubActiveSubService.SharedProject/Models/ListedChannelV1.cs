namespace PubActiveSubService.Models {
    public class ListedChannelV1 {
        public string ChannelName { get; set; } = string.Empty;

        public string[] Subscribers { get; set; } = new string[] { };
    }
}
