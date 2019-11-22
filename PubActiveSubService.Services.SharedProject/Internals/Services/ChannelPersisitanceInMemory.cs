using PubActiveSubService.Internals.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;

namespace PubActiveSubService.Internals.Services {
    public class ChannelPersisitanceInMemory : IChannelPersisitance {
        private static readonly MemoryChannels TheMemoryChannels = new MemoryChannels();

        private readonly IAppSettingsReader AppSettingsReader;


        public ChannelPersisitanceInMemory(IAppSettingsReader appSettingsReader) {
            if (null == appSettingsReader) throw new ArgumentNullException(nameof(appSettingsReader));

            AppSettingsReader = appSettingsReader;
        }

        public IEnumerable<Models.ListedChannel> ListChannels(Models.ChannelSearch channelSearch) => throw new NotImplementedException();

        public string[] LookupSubscriberUrlsByChanneNamel(string channelName, params string[] internalUrls) => throw new NotImplementedException();

        public void PostChannelName(string channelName) => TheMemoryChannels.PostChannelName(channelName);

        public void Subscribe(Models.Subscribe subscribe, string defaultInternalUrl) => throw new NotImplementedException();

        public void Unsubscribe(Models.Unsubscribe unsubscribe) => throw new NotImplementedException();

        #region Private classes
        private class MemoryChannels {
            private readonly Dictionary<string, MemoryChannel> Dictionary = new Dictionary<string, MemoryChannel>();
            public void PostChannelName(string channelName) {
                if (Dictionary.ContainsKey(channelName))
                    return;

                var MemoryChannel = new MemoryChannel() { ChannelName = channelName };
                while (!Dictionary.TryAdd(MemoryChannel.ChannelName, MemoryChannel)) { }
            }

            public IEnumerable<MemoryChannel> ListChannels(string search) {
                foreach (var key in Dictionary.Keys.ToArray()) {
                    MemoryChannel memoryChannel;
                    while (!Dictionary.TryGetValue(key, out memoryChannel)) { }
                    yield return memoryChannel;
                }
                yield break;
            }
        }

        private class MemoryChannel {
            private readonly Dictionary<string, MemorySubscriber> Dictionary = new Dictionary<string, MemorySubscriber>();
            public string ChannelName { get; set; } = string.Empty;

            public IEnumerable<MemorySubscriber> ListSubscribers(string subscriberName) {
                foreach (var key in Dictionary.Keys.ToArray()) {
                    MemorySubscriber memorySubscriber;
                    while (!Dictionary.TryGetValue(key, out memorySubscriber)) { }
                    yield return memorySubscriber;
                }
                yield break; ;
            }
        }

        private class MemorySubscriber {
            private readonly Queue<Models.PublishPackage> PublishPackageQueue = new Queue<Models.PublishPackage>();
            public string SubscriberName { get; set; } = string.Empty;
            public bool Enabled { get; set; } = false;
            public string SubscriberPostUrl { get; set; } = string.Empty;
        }
        #endregion
    }
}
