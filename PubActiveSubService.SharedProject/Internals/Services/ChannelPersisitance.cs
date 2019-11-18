using PubActiveSubService.Internals.Interfaces;
using PubActiveSubService.Library;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PubActiveSubService.Internals.Services {
    public class ChannelPersisitance : IChannelPersisitance {
        public string[] LookupSubscriberUrlsByChanneNamel(string channelName, params string[] internalUrls) {
            channelName = channelName.ToEnforceChannelNamingConventions();

            var collection = new Collection<string>();

            var channelsV1 = ChannelBadNasFileInfo.Read();
            foreach (var channelV1 in channelsV1.Channels)
                if (channelName == channelV1.ChannelName)
                    foreach (var subscriberV1 in channelV1.Subscribes)
                        if (subscriberV1.Enabled)
                            if (subscriberV1.SubscriberPostUrl.Length > 0)
                                collection.Add(subscriberV1.SubscriberPostUrl);

            foreach (var internalUrl in internalUrls)
                if (internalUrl.Length > 0)
                    collection.Add(internalUrl);

            return collection.ToArray();
        }

        public void PostChannelName(string channelName) {
            channelName = channelName.ToEnforceChannelNamingConventions();

            var channelsV1 = ChannelBadNasFileInfo.Read();
            foreach (var channelV1 in channelsV1.Channels)
                if (channelName == channelV1.ChannelName)
                    return;

            channelsV1.Channels.Add(new Models.ChannelV1() { ChannelName = channelName });
            ChannelBadNasFileInfo.Write(channelsV1);
        }

        public IEnumerable<Models.ListedChannelV1> ListChannels(Models.ListChannelsV1 listChannelsV1) {
            var channelName = listChannelsV1.ChannelSearch.ToEnforceChannelNamingConventions();

            var channelsV1 = ChannelBadNasFileInfo.Read();
            foreach (var channelV1 in channelsV1.Channels)
                if (channelName == channelV1.ChannelName)
                    yield return new Models.ListedChannelV1() {
                        ChannelName = channelV1.ChannelName,
                        Subscribers = channelV1.Subscribes
                            .Select(subscriber => subscriber.SubscriberName)
                                .ToArray()
                    };
        }
    }
}
