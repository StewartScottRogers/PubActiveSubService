using PubActiveSubService.Internals.Interfaces;
using PubActiveSubService.Library;

using System.Collections.ObjectModel;
using System.Linq;

namespace PubActiveSubService.Internals.Services {
    public class ChannelPersisitance : IChannelPersisitance {
        public string[] LookupSubscriberUrlsByChannel(string channelName, params string[] internalUrls) {
            channelName = channelName.ToEnforceChannelNamingConventions();

            var collection = new Collection<string>();

            var channelsV1 = ChannelBadNasFileInfo.Read();
            foreach (var channelV1 in channelsV1.Channels)
                if (channelName == channelV1.Name)
                    foreach (var subscriberV1 in channelV1.Subscribes)
                        if (subscriberV1.Enabled)
                            if (subscriberV1.SubscriberPostUrl.Length > 0)
                                collection.Add(subscriberV1.SubscriberPostUrl);

            foreach (var internalUrl in internalUrls)
                if (internalUrl.Length > 0)
                    collection.Add(internalUrl);

            return collection.ToArray();
        }
    }
}
