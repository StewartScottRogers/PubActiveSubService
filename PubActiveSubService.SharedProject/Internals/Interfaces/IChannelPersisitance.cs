namespace PubActiveSubService.Internals.Interfaces {
    public interface IChannelPersisitance {
        string[] LookupSubscriberUrlsByChanneNamel(string channelName, params string[] internalUrls);
        void PostChannelName(string channelName);
    }
}
