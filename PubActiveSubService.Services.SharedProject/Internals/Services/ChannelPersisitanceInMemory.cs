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

        public IEnumerable<Models.Channel> ListChannels(Models.Search search) {
            using (var ReadLock = InMemoryDatabase.ReaderWriterLockSlim.ReadLock()) {
                foreach (var inMemoryChannel in InMemoryDatabase.InMemoryChannels.Search(search.SearchPattern)) {
                    var subscriberCollection = new Collection<Models.Subscriber>();
                    foreach (var inMemorySubscriber in inMemoryChannel.InMemorySubscribers)
                        subscriberCollection.Add(new Models.Subscriber() { SubscriberName = inMemorySubscriber.SubscriberName, Enabled = inMemorySubscriber.Enabled, RestUrl = inMemorySubscriber.RestUrl });
                    yield return new Models.Channel() { ChannelName = inMemoryChannel.ChannelName, Subscribers = subscriberCollection.ToList() };
                }
            }
        }

        public string[] LookupSubscriberUrlsByChanneNamel(string channelName, params string[] defaultInternalUrls) {
            using (var ReadLock = InMemoryDatabase.ReaderWriterLockSlim.ReadLock()) {
                return InMemoryDatabase.InMemoryChannels.Search(channelName)
                .ToArray()
                    .SelectMany(channel => channel.InMemorySubscribers)
                        .Select(inMemorySubscriber => inMemorySubscriber.RestUrl)
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
                                                                        RestUrl = subscribe.RestUrl.Length > 0 ?
                                                                                                subscribe.RestUrl : defaultInternalUrl.ToEnforcedUrlNamingStandards()
                                                                    }
                                                               );
                    }
                    break;
                }
            }
        }

        public void Unsubscribe(Models.SubscriberBinding subscriberBinding) {
            using (var writeLock = InMemoryDatabase.ReaderWriterLockSlim.WriteLock()) {
                foreach (var inMemoryChannel in InMemoryDatabase.InMemoryChannels.Lookup(subscriberBinding.ChannelName).ToArray())
                    foreach (var inMemorySubscriber in inMemoryChannel.InMemorySubscribers)
                        if (inMemorySubscriber.SubscriberName == subscriberBinding.SubscriberName) {
                            inMemorySubscriber.Enabled = false;
                            inMemorySubscriber.PackageQueue.Clear();
                        }
            }
        }
    }
}
