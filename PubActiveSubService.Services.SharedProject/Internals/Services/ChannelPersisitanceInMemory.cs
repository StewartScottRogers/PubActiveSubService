using PubActiveSubService.Internals.Interfaces;
using PubActiveSubService.Internals.Services.Library;

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
            using (var ReadLock = InMemoryChannels.ReaderWriterLockSlim.ReadLock()) {
                foreach (var channelQueue in InMemoryChannels.ChannelMemmoryCollection.Search(channelSearch.Search)) {
                    var subscriberCollection = new Collection<Models.Subscriber>();
                    foreach (var subscriber in channelQueue.Subscribers)
                        subscriberCollection.Add(new Models.Subscriber() {  SubscriberName= subscriber.SubscriberName, Enabled= subscriber.Enabled, SubscriberPostUrl= subscriber.SubscriberPostUrl });
                    yield return new Models.Channel() { ChannelName = channelQueue.ChannelName, Subscribers = subscriberCollection.ToList() };
                }                    
            }
        }

        public string[] LookupSubscriberUrlsByChanneNamel(string channelName, params string[] defaultInternalUrls) {
            using (var ReadLock = InMemoryChannels.ReaderWriterLockSlim.ReadLock()) {
                return InMemoryChannels.ChannelMemmoryCollection.Search(channelName)
                .ToArray()
                    .SelectMany(channel => channel.Subscribers)
                        .Select(subscriber => subscriber.SubscriberPostUrl)
                            .Concat(defaultInternalUrls).ToArray();
            }
        }

        public void PostChannelName(string channelName) {
            using (var upgadableReadLock = InMemoryChannels.ReaderWriterLockSlim.UpgadableReadLock()) {
                if (InMemoryChannels.ChannelMemmoryCollection.Lookup(channelName).Count() <= 0)
                    using (var writeLock = upgadableReadLock.WriteLock()) {
                        InMemoryChannels.ChannelMemmoryCollection.Add(new ChannelMemmory() { ChannelName = channelName.Trim() });
                    }
            }
        }

        public void Subscribe(Models.Subscribe subscribe, string defaultInternalUrl) {
            using (var upgadableReadLock = InMemoryChannels.ReaderWriterLockSlim.UpgadableReadLock()) {
                foreach (var channel in InMemoryChannels.ChannelMemmoryCollection.Lookup(subscribe.ChannelName).ToArray()) {
                    using (var writeLock = upgadableReadLock.WriteLock()) {
                        channel.Subscribers.Add(
                                new SubscriberQueue() {
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
            using (var writeLock = InMemoryChannels.ReaderWriterLockSlim.WriteLock()) {
                foreach (var channel in InMemoryChannels.ChannelMemmoryCollection.Lookup(unsubscribe.ChannelName).ToArray())
                    foreach (var subscriber in channel.Subscribers)
                        if (subscriber.SubscriberName == unsubscribe.SubscriberName) {
                            subscriber.Enabled = false;
                            subscriber.PublishPackageQueue.Clear();
                        }
            }
        }
    }
}
