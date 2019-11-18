namespace PubActiveSubService.Internals.Interfaces {
    public interface IChannelPersisitance {
        string[] LookupSubscriberUrlsByChannel(string Channel, params string[] internalUrls);
    }
}
