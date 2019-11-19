using PubActiveSubService.Internals.Interfaces;
using PubActiveSubService.Library;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PubActiveSubService.Internals.Services {
    public class ChannelPersisitance : IChannelPersisitance {
        public string[] LookupSubscriberUrlsByChanneNamel(string channelName, params string[] internalUrls) {
            lock (this) {
                channelName = channelName.ToEnforcedChannelNamingConventions();

                var collection = new Collection<string>();

                var channels = ChannelBadNasFileInfo.Read();
                foreach (var channel in channels.ChannelList)
                    if (channelName == channel.ChannelName)
                        foreach (var subscriber in channel.Subscribers)
                            if (subscriber.Enabled)
                                if (subscriber.SubscriberPostUrl.Length > 0)
                                    collection.Add(subscriber.SubscriberPostUrl);

                foreach (var internalUrl in internalUrls)
                    if (internalUrl.Length > 0)
                        collection.Add(internalUrl);

                return collection.ToArray();
            }
        }

        public void PostChannelName(string channelName) {
            lock (this) {
                channelName = channelName.ToEnforcedChannelNamingConventions();

                var channels = ChannelBadNasFileInfo.Read();
                foreach (var channel in channels.ChannelList)
                    if (channelName == channel.ChannelName)
                        return;

                channels.ChannelList.Add(new Models.Channel() { ChannelName = channelName });
                ChannelBadNasFileInfo.Write(channels);
            }
        }

        public IEnumerable<Models.ListedChannel> ListChannels(Models.ChannelSearch channelSearch) {
            lock (this) {
                var search = channelSearch.Search.ToEnforceChannelSearchNamingConventions();

                var channelArray = ChannelBadNasFileInfo.Read().ChannelList.ToArray();
                foreach (var channel in channelArray)
                    if (
                            search == channel.ChannelName
                            || search.Length <= 0
                            || search.Trim() == "*"
                       )
                        yield return new Models.ListedChannel() {
                            ChannelName = channel.ChannelName,
                            Subscribers = channel.Subscribers
                        };
            }
        }

        public void Subscribe(Models.Subscribe subscribe, string defaultInternalUr) {
            lock (this) {
                var channelName = subscribe.ChannelName.ToEnforcedChannelNamingConventions();
                var subscriberName = subscribe.SubscriberName.ToEnforcedSubscriberNamingConventions();

                var channels = ChannelBadNasFileInfo.Read();
                foreach (var channel in channels.ChannelList.ToArray())
                    if (channelName == channel.ChannelName) {
                        foreach (var subscriber in channel.Subscribers) {
                            if (subscriber.SubscriberName == subscriberName) {
                                subscriber.Enabled = subscribe.Enabled;
                                subscriber.SubscriberPostUrl = subscribe.SubscriberPostUrl.Trim();
                                ChannelBadNasFileInfo.Write(channels);
                                return;
                            }
                        }

                        channel.Subscribers.Add(
                                                    new Models.Subscriber() {
                                                        SubscriberName = subscriberName,
                                                        Enabled = subscribe.Enabled,
                                                        SubscriberPostUrl = subscribe.SubscriberPostUrl.Length > 0 ? subscribe.SubscriberPostUrl.Trim() : defaultInternalUr.Trim()
                                                    }
                                                 );
                        ChannelBadNasFileInfo.Write(channels);
                        return;
                    }
            }
        }

        public void Unsubscribe(Models.Unsubscribe unsubscribe) {
            lock (this) {
                var channelName = unsubscribe.ChannelName.ToEnforcedChannelNamingConventions();
                var subscriberName = unsubscribe.SubscriberName.ToEnforcedSubscriberNamingConventions();

                var channels = ChannelBadNasFileInfo.Read();
                foreach (var channel in channels.ChannelList.ToArray())
                    if (channelName == channel.ChannelName) {
                        foreach (var subscriber in channel.Subscribers.ToArray()) {
                            if (subscriber.SubscriberName == subscriberName) {
                                channel.Subscribers.Remove(subscriber);
                                ChannelBadNasFileInfo.Write(channels);
                                return;
                            }
                        }
                    }
            }
        }
    }
}
