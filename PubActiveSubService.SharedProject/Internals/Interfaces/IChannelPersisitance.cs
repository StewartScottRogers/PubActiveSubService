using System.Collections.Generic;

namespace PubActiveSubService.Internals.Interfaces {
    public interface IChannelPersisitance {
        string[] LookupSubscriberUrlsByChanneNamel(string channelName, params string[] internalUrls);
        void PostChannelName(string channelName);
        IEnumerable<Models.ListedChannelV1> ListChannels(Models.SearchV1 searchV1);
        void Subscribe(Models.SubscribeV1 subscribeV1);
        void Unsubscribe(Models.UnsubscribeV1 unsubscribeV1);
    }
}
