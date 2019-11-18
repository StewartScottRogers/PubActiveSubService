using System.Collections.Generic;

namespace PubActiveSubService.Internals.Interfaces {
    public interface IChannelPersisitance {
        string[] LookupSubscriberUrlsByChanneNamel(string channelName, params string[] internalUrls);
        void PostChannelName(string channelName);
        IEnumerable<Models.ListedChannelV1> ListChannels(Models.ListChannelsV1 listChannelsV1);
    }
}
