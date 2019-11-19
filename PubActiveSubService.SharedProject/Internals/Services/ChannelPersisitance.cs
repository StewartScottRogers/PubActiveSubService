using PubActiveSubService.Internals.Interfaces;
using PubActiveSubService.Library;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PubActiveSubService.Internals.Services {
    public class ChannelPersisitance : IChannelPersisitance {
        public string[] LookupSubscriberUrlsByChanneNamel(string channelName, params string[] internalUrls) {
            channelName = channelName.ToEnforcedChannelNamingConventions();

            var collection = new Collection<string>();

            var channelsV1 = ChannelBadNasFileInfo.Read();
            foreach (var channelV1 in channelsV1.Channels)
                if (channelName == channelV1.ChannelName)
                    foreach (var subscriberV1 in channelV1.Subscribers)
                        if (subscriberV1.Enabled)
                            if (subscriberV1.SubscriberPostUrl.Length > 0)
                                collection.Add(subscriberV1.SubscriberPostUrl);

            foreach (var internalUrl in internalUrls)
                if (internalUrl.Length > 0)
                    collection.Add(internalUrl);

            return collection.ToArray();
        }

        public void PostChannelName(string channelName) {
            channelName = channelName.ToEnforcedChannelNamingConventions();

            var channelsV1 = ChannelBadNasFileInfo.Read();
            foreach (var channelV1 in channelsV1.Channels)
                if (channelName == channelV1.ChannelName)
                    return;

            channelsV1.Channels.Add(new Models.ChannelV1() { ChannelName = channelName });
            ChannelBadNasFileInfo.Write(channelsV1);
        }

        public IEnumerable<Models.ListedChannelV1> ListChannels(Models.SearchV1 searchV1) {
            var channelSearch = searchV1.ChannelSearch.ToEnforceChannelSearchNamingConventions();

            var channelV1Array = ChannelBadNasFileInfo.Read().Channels.ToArray();
            foreach (var channelV1 in channelV1Array)
                if (
                        channelSearch == channelV1.ChannelName
                        || channelSearch.Length <= 0
                        || channelSearch.Trim() == "*"
                   )
                    yield return new Models.ListedChannelV1() {
                        ChannelName = channelV1.ChannelName,
                        Subscribers = channelV1.Subscribers
                    };
        }

        public void Subscribe(Models.SubscribeV1 subscribeV1) {
            var channelName = subscribeV1.ChannelName.ToEnforcedChannelNamingConventions();
            var channelsV1 = ChannelBadNasFileInfo.Read();

            foreach (var channelV1 in channelsV1.Channels.ToArray())
                if (channelName == channelV1.ChannelName) {
                    foreach (var subscriber in channelV1.Subscribers) {
                        if (subscriber.SubscriberName == subscribeV1.SubscriberName) {
                            subscriber.Enabled = subscribeV1.Enabled;
                            subscriber.SubscriberPostUrl = subscribeV1.SubscriberPostUrl.Trim();
                            ChannelBadNasFileInfo.Write(channelsV1);
                            return;
                        }
                    }
                    channelV1.Subscribers.Add(
                                                new Models.SubscriberV1() {
                                                    SubscriberName = subscribeV1.SubscriberName,
                                                    Enabled = subscribeV1.Enabled,
                                                    SubscriberPostUrl = subscribeV1.SubscriberPostUrl.Trim()
                                                }
                                             );
                    ChannelBadNasFileInfo.Write(channelsV1);
                    return;
                }
        }

        public void Unsubscribe(Models.UnsubscribeV1 unsubscribeV1) {

        }
    }
}
