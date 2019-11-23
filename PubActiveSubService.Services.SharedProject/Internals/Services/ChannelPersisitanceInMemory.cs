using PubActiveSubService.Internals.Interfaces;
using PubActiveSubService.Internals.Services.LibraryInMemory;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;

namespace PubActiveSubService.Internals.Services {
    public class ChannelPersisitanceInMemory : IChannelPersisitance {
        private readonly IAppSettingsReader AppSettingsReader;

        public ChannelPersisitanceInMemory(IAppSettingsReader appSettingsReader) {
            if (null == appSettingsReader) throw new ArgumentNullException(nameof(appSettingsReader));

            AppSettingsReader = appSettingsReader;
        }

        public IEnumerable<Models.Channel> ListChannels(Models.ChannelSearch channelSearch) {
            using (var ReadLock = InMemoryDatabase.ReaderWriterLockSlim.ReadLock()) {
                foreach (var inMemoryChannel in InMemoryDatabase.InMemoryChannels.Search(channelSearch.Search)) {
                    var subscriberCollection = new Collection<Models.Subscriber>();
                    foreach (var inMemorySubscriber in inMemoryChannel.InMemorySubscribers)
                        subscriberCollection.Add(new Models.Subscriber() { SubscriberName = inMemorySubscriber.SubscriberName, Enabled = inMemorySubscriber.Enabled, SubscriberPostUrl = inMemorySubscriber.SubscriberPostUrl });
                    yield return new Models.Channel() { ChannelName = inMemoryChannel.ChannelName, Subscribers = subscriberCollection.ToList() };
                }
            }
        }

        public string[] LookupSubscriberUrlsByChanneNamel(string channelName, params string[] defaultInternalUrls) {
            using (var ReadLock = InMemoryDatabase.ReaderWriterLockSlim.ReadLock()) {
                return InMemoryDatabase.InMemoryChannels.Search(channelName)
                .ToArray()
                    .SelectMany(channel => channel.InMemorySubscribers)
                        .Select(inMemorySubscriber => inMemorySubscriber.SubscriberPostUrl)
                            .Concat(defaultInternalUrls).ToArray();
            }
        }

        public void PostChannelName(string channelName) {
            using (var upgadableReadLock = InMemoryDatabase.ReaderWriterLockSlim.UpgadableReadLock()) {
                if (InMemoryDatabase.InMemoryChannels.Lookup(channelName).Count() <= 0)
                    using (var writeLock = upgadableReadLock.WriteLock()) {
                        InMemoryDatabase.InMemoryChannels.Add(new InMemoryChannel() { ChannelName = channelName.Trim() });
                    }
            }
        }

        public void Subscribe(Models.Subscribe subscribe, string defaultInternalUrl) {
            using (var upgadableReadLock = InMemoryDatabase.ReaderWriterLockSlim.UpgadableReadLock()) {
                foreach (var inMemoryChannel in InMemoryDatabase.InMemoryChannels.Lookup(subscribe.ChannelName).ToArray()) {
                    using (var writeLock = upgadableReadLock.WriteLock()) {
                        inMemoryChannel.InMemorySubscribers.Add(
                                                                    new InMemorySubscriber() {
                                                                        SubscriberName = subscribe.SubscriberName.ToEnforcedSubscriberNamingConventions(),
                                                                        Enabled = true,
                                                                        SubscriberPostUrl = subscribe.SubscriberPostUrl.Length > 0 ?
                                                                                                subscribe.SubscriberPostUrl : defaultInternalUrl.ToEnforcedUrlNamingStandards()
                                                                    }
                                                               );
                    }
                    break;
                }
            }
        }

        public void Unsubscribe(Models.Unsubscribe unsubscribe) {
            using (var writeLock = InMemoryDatabase.ReaderWriterLockSlim.WriteLock()) {
                foreach (var inMemoryChannel in InMemoryDatabase.InMemoryChannels.Lookup(unsubscribe.ChannelName).ToArray())
                    foreach (var inMemorySubscriber in inMemoryChannel.InMemorySubscribers)
                        if (inMemorySubscriber.SubscriberName == unsubscribe.SubscriberName) {
                            inMemorySubscriber.Enabled = false;
                            inMemorySubscriber.PublishPackageQueue.Clear();
                        }
            }
        }
    }
}
