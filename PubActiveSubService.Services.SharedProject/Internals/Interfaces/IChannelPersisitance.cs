using System.Collections.Generic;

namespace PubActiveSubService.Internals.Interfaces {
    public interface IChannelPersisitance {
        string[] LookupSubscriberUrlsByChanneNamel(string channelName, params string[] internalUrls);
        void PostChannelName(string channelName);
        IEnumerable<Models.Channel> ListChannels(Models.Search search);
        void Subscribe(Models.Subscribe subscribe, string defaultInternalUrl);
        void Unsubscribe(Models.Unsubscribe unsubscribe);
    }
}
