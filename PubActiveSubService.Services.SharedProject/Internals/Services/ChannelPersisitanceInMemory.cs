using PubActiveSubService.Internals.Interfaces;
using PubActiveSubService.Internals.Services.Library;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace PubActiveSubService.Internals.Services {
    public class ChannelPersisitanceInMemory : IChannelPersisitance {
        private static readonly ReaderWriterLockSlim ReaderWriterLockSlim = new ReaderWriterLockSlim();
        private readonly IAppSettingsReader AppSettingsReader;

        public ChannelPersisitanceInMemory(IAppSettingsReader appSettingsReader) {
            if (null == appSettingsReader) throw new ArgumentNullException(nameof(appSettingsReader));

            AppSettingsReader = appSettingsReader;
        }

        public IEnumerable<Models.Channel> ListChannels(Models.ChannelSearch channelSearch) {
            using (var ReadLock = ReaderWriterLockSlim.ReadLock()) {
                return QueuedChannelCollectionManager.QueuedChannelCollection.Search(channelSearch.Search);
            }
        }

        public string[] LookupSubscriberUrlsByChanneNamel(string channelName, params string[] defaultInternalUrls) {
            using (var ReadLock = ReaderWriterLockSlim.ReadLock()) {
                return QueuedChannelCollectionManager.QueuedChannelCollection.Search(channelName)
                .ToArray()
                    .SelectMany(channel => channel.Subscribers)
                        .Select(subscriber => subscriber.SubscriberPostUrl)
                            .Concat(defaultInternalUrls).ToArray();
            }
        }

        public void PostChannelName(string channelName) {
            using (var upgadableReadLock = ReaderWriterLockSlim.UpgadableReadLock()) {
                if (QueuedChannelCollectionManager.QueuedChannelCollection.Lookup(channelName).Count() <= 0)
                    using (var writeLock = upgadableReadLock.WriteLock()) {
                        QueuedChannelCollectionManager.QueuedChannelCollection.Add(new ChannelQueue() { ChannelName = channelName.Trim() });
                    }
            }
        }

        public void Subscribe(Models.Subscribe subscribe, string defaultInternalUrl) {
            using (var upgadableReadLock = ReaderWriterLockSlim.UpgadableReadLock()) {
                foreach (var channel in QueuedChannelCollectionManager.QueuedChannelCollection.Lookup(subscribe.ChannelName).ToArray()) {
                    using (var writeLock = upgadableReadLock.WriteLock()) {
                        channel.Subscribers.Add(
                                new Models.Subscriber() {
                                    SubscriberName = subscribe.SubscriberName.ToEnforcedSubscriberNamingConventions(),
                                    Enabled = true,
                                    SubscriberPostUrl = subscribe.SubscriberPostUrl.Length > 0 ?
                                                            subscribe.SubscriberPostUrl : defaultInternalUrl.ToEnforcedUrlNamingStandards()
                                });
                    }
                    break;
                }
            }
        }

        public void Unsubscribe(Models.Unsubscribe unsubscribe) {
            using (var writeLock = ReaderWriterLockSlim.WriteLock()) {
                foreach (var channel in QueuedChannelCollectionManager.QueuedChannelCollection.Lookup(unsubscribe.ChannelName).ToArray())
                    foreach (var subscriber in channel.Subscribers)
                        if (subscriber.SubscriberName == unsubscribe.SubscriberName)
                            subscriber.Enabled = false;
            }
        }
    }
}
