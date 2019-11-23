using PubActiveSubService.Internals.Interfaces;
using PubActiveSubService.Internals.Services.Library;

using System;
using System.Collections.Generic;
using System.Linq;

namespace PubActiveSubService.Internals.Services {
    public class ChannelPersisitanceInMemory : IChannelPersisitance {
        private readonly IAppSettingsReader AppSettingsReader;

        public ChannelPersisitanceInMemory(IAppSettingsReader appSettingsReader) {
            if (null == appSettingsReader) throw new ArgumentNullException(nameof(appSettingsReader));

            AppSettingsReader = appSettingsReader;
        }

        public IEnumerable<Models.Channel> ListChannels(Models.ChannelSearch channelSearch) {
            lock (this) {
                return QueuedChannelCollectionManager.QueuedChannelCollection.Search(channelSearch.Search);
            }
        }

        public string[] LookupSubscriberUrlsByChanneNamel(string channelName, params string[] defaultInternalUrls) {
            lock (this) {
                return QueuedChannelCollectionManager.QueuedChannelCollection.Search(channelName)
                .ToArray()
                    .SelectMany(channel => channel.Subscribers)
                        .Select(subscriber => subscriber.SubscriberPostUrl)
                            .Concat(defaultInternalUrls).ToArray();
            }
        }

        public void PostChannelName(string channelName) {
            lock (this) {
                if (QueuedChannelCollectionManager.QueuedChannelCollection.Lookup(channelName).Count() <= 0)
                    QueuedChannelCollectionManager.QueuedChannelCollection.Add(new ChannelQueue() { ChannelName = channelName.Trim() });
            }
        }

        public void Subscribe(Models.Subscribe subscribe, string defaultInternalUrl) {
            lock (this) {
                foreach (var channel in QueuedChannelCollectionManager.QueuedChannelCollection.Lookup(subscribe.ChannelName).ToArray()) {
                    channel.Subscribers.Add(
                            new Models.Subscriber() {
                                SubscriberName = subscribe.SubscriberName.ToEnforcedSubscriberNamingConventions(),
                                Enabled = true,
                                SubscriberPostUrl = subscribe.SubscriberPostUrl.Length > 0 ?
                                                        subscribe.SubscriberPostUrl : defaultInternalUrl.ToEnforcedUrlNamingStandards()
                            });
                    break;
                }
            }
        }

        public void Unsubscribe(Models.Unsubscribe unsubscribe) {
            lock (this) {
                foreach (var channel in QueuedChannelCollectionManager.QueuedChannelCollection.Lookup(unsubscribe.ChannelName).ToArray())
                    foreach (var subscriber in channel.Subscribers)
                        if (subscriber.SubscriberName == unsubscribe.SubscriberName)
                            subscriber.Enabled = false;
            }
        }
    }
}
