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

                var channels = ChannelFileInfo.Read();
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

                var channels = ChannelFileInfo.Read();
                foreach (var channel in channels.ChannelList)
                    if (channelName == channel.ChannelName)
                        return;

                channels.ChannelList.Add(new Models.Channel() { ChannelName = channelName });
                ChannelFileInfo.Write(channels);
            }
        }

        public IEnumerable<Models.ListedChannel> ListChannels(Models.ChannelSearch channelSearch) {
            lock (this) {
                channelSearch.Search = channelSearch.Search.ToEnforceChannelSearchNamingConventions();

                var channelArray = ChannelFileInfo.Read().ChannelList.ToArray();
                foreach (var channel in channelArray)
                    if (
                            channelSearch.Search == channel.ChannelName
                            || channelSearch.Search.Length <= 0
                            || channelSearch.Search.Trim() == "*"
                       )
                        yield return new Models.ListedChannel() {
                            ChannelName = channel.ChannelName,
                            Subscribers = channel.Subscribers
                        };
            }
        }

        public void Subscribe(Models.Subscribe subscribe, string defaultInternalUr) {
            lock (this) {
                subscribe.SubscriberPostUrl = subscribe.SubscriberPostUrl.ToEnforcedUrlNamingStandards();
                subscribe.ChannelName = subscribe.ChannelName.ToEnforcedChannelNamingConventions();
                subscribe.SubscriberName = subscribe.SubscriberName.ToEnforcedSubscriberNamingConventions();
                
                var channels = ChannelFileInfo.Read();
                foreach (var channel in channels.ChannelList.ToArray())
                    if (subscribe.ChannelName == channel.ChannelName) {
                        foreach (var subscriber in channel.Subscribers) {
                            if (subscriber.SubscriberName == subscribe.SubscriberName) {
                                subscriber.Enabled = subscribe.Enabled;
                                subscriber.SubscriberPostUrl
                                                            = subscribe.SubscriberPostUrl.Length > 0 ?
                                                                subscribe.SubscriberPostUrl : defaultInternalUr.ToEnforcedUrlNamingStandards();

                                ChannelFileInfo.Write(channels);
                                return;
                            }
                        }

                        channel.Subscribers.Add(
                                                    new Models.Subscriber() {
                                                        SubscriberName = subscribe.SubscriberName,
                                                        Enabled = subscribe.Enabled,
                                                        SubscriberPostUrl 
                                                            = subscribe.SubscriberPostUrl.Length > 0 ?
                                                                subscribe.SubscriberPostUrl : defaultInternalUr.ToEnforcedUrlNamingStandards()
                                                    }
                                               );

                        ChannelFileInfo.Write(channels);
                        return;
                    }
            }
        }

        public void Unsubscribe(Models.Unsubscribe unsubscribe) {
            lock (this) {
                var channelName = unsubscribe.ChannelName.ToEnforcedChannelNamingConventions();
                var subscriberName = unsubscribe.SubscriberName.ToEnforcedSubscriberNamingConventions();

                var channels = ChannelFileInfo.Read();
                foreach (var channel in channels.ChannelList.ToArray())
                    if (channelName == channel.ChannelName) {
                        foreach (var subscriber in channel.Subscribers.ToArray()) {
                            if (subscriber.SubscriberName == subscriberName) {
                                channel.Subscribers.Remove(subscriber);
                                ChannelFileInfo.Write(channels);
                                return;
                            }
                        }
                    }
            }
        }
    }
}
