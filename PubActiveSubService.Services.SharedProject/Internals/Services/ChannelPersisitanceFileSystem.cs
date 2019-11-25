using PubActiveSubService.Internals.Interfaces;
using PubActiveSubService.Library;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;

namespace PubActiveSubService.Internals.Services {
    public class ChannelPersisitanceFileSystem : IChannelPersisitance {
        private static readonly ReaderWriterLockSlim ReaderWriterLockSlim = new ReaderWriterLockSlim();
        private readonly IAppSettingsReader AppSettingsReader;

        public ChannelPersisitanceFileSystem(IAppSettingsReader appSettingsReader) {
            if (null == appSettingsReader) throw new ArgumentNullException(nameof(appSettingsReader));

            AppSettingsReader = appSettingsReader;
        }

        public string[] LookupSubscriberUrlsByChanneNamel(string channelName, params string[] internalUrls) {
            using (var ReadLock = ReaderWriterLockSlim.ReadLock()) {
                channelName = channelName.ToEnforcedChannelNamingConventions();

                var collection = new Collection<string>();

                var channels = ChannelFileInfo.Read();
                foreach (var channel in channels.ChannelList)
                    if (channelName == channel.ChannelName)
                        foreach (var subscriber in channel.Subscribers)
                            if (subscriber.Enabled)
                                if (subscriber.RestUrl.Length > 0)
                                    collection.Add(subscriber.RestUrl);

                foreach (var internalUrl in internalUrls)
                    if (internalUrl.Length > 0)
                        collection.Add(internalUrl);

                return collection.ToArray();
            }
        }

        public IEnumerable<Models.Channel> ListChannels(Models.Search search) {
            using (var ReadLock = ReaderWriterLockSlim.ReadLock()) {
                search.SearchPattern = search.SearchPattern.ToEnforceChannelSearchPatternConventions();

                var channelArray = ChannelFileInfo.Read().ChannelList.ToArray();
                foreach (var channel in channelArray)
                    if (
                            search.SearchPattern == channel.ChannelName
                            || search.SearchPattern.Length <= 0
                            || search.SearchPattern.Trim() == "*"
                       )
                        yield return new Models.Channel() {
                            ChannelName = channel.ChannelName,
                            Subscribers = channel.Subscribers
                        };
            }
        }

        public void PostChannelName(string channelName) {
            using (var upgadableReadLock = ReaderWriterLockSlim.UpgadableReadLock()) {
                channelName = channelName.ToEnforcedChannelNamingConventions();

                var channels = ChannelFileInfo.Read();
                foreach (var channel in channels.ChannelList)
                    if (channelName == channel.ChannelName)
                        return;

                channels.ChannelList.Add(new Models.Channel() { ChannelName = channelName });
                using (var WriteLock = upgadableReadLock.WriteLock()) {
                    ChannelFileInfo.Write(channels);
                }
            }
        }



        public void Subscribe(Models.Subscribe subscribe, string defaultInternalUrl) {
            using (var upgadableReadLock = ReaderWriterLockSlim.UpgadableReadLock()) {
                subscribe.RestUrl = subscribe.RestUrl.ToEnforcedUrlNamingStandards();
                subscribe.ChannelName = subscribe.ChannelName.ToEnforcedChannelNamingConventions();
                subscribe.SubscriberName = subscribe.SubscriberName.ToEnforcedSubscriberNamingConventions();

                var channels = ChannelFileInfo.Read();
                foreach (var channel in channels.ChannelList.ToArray())
                    if (subscribe.ChannelName == channel.ChannelName) {
                        foreach (var subscriber in channel.Subscribers) {
                            if (subscriber.SubscriberName == subscribe.SubscriberName) {
                                subscriber.Enabled = subscribe.Enabled;
                                subscriber.RestUrl = subscribe.RestUrl.Length > 0 ?
                                                                    subscribe.RestUrl : defaultInternalUrl.ToEnforcedUrlNamingStandards();

                                ChannelFileInfo.Write(channels);
                                return;
                            }
                        }

                        channel.Subscribers.Add(
                                                    new Models.Subscriber() {
                                                        SubscriberName = subscribe.SubscriberName,
                                                        Enabled = subscribe.Enabled,
                                                        RestUrl
                                                            = subscribe.RestUrl.Length > 0 ?
                                                                subscribe.RestUrl : defaultInternalUrl.ToEnforcedUrlNamingStandards()
                                                    }
                                               );

                        using (var WriteLock = upgadableReadLock.WriteLock()) {
                            ChannelFileInfo.Write(channels);
                        }
                        return;
                    }
            }
        }

        public void Unsubscribe(Models.SubscriberBinding subscriberBinding) {
            using (var upgadableReadLock = ReaderWriterLockSlim.UpgadableReadLock()) {
                var channelName = subscriberBinding.ChannelName.ToEnforcedChannelNamingConventions();
                var subscriberName = subscriberBinding.SubscriberName.ToEnforcedSubscriberNamingConventions();

                var channels = ChannelFileInfo.Read();
                foreach (var channel in channels.ChannelList.ToArray())
                    if (channelName == channel.ChannelName) {
                        foreach (var subscriber in channel.Subscribers.ToArray()) {
                            if (subscriber.SubscriberName == subscriberName) {
                                channel.Subscribers.Remove(subscriber);
                                using (var WriteLock = upgadableReadLock.WriteLock()) {
                                    ChannelFileInfo.Write(channels);
                                }
                                return;
                            }
                        }
                    }
            }
        }
    }
}
