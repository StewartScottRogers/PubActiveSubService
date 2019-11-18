using Newtonsoft.Json;

using PubActiveSubService.Internals.Interfaces;

using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;

namespace PubActiveSubService.Internals.Services {
    public class ChannelPersisitance : IChannelPersisitance {
        public string[] LookupSubscriberUrlsByChannel(string channel, params string[] internalUrls) {
            var collection = new Collection<string>();

            var channelsV1 = ChannelBadNasFileInfo.Read();
            foreach (var channelV1 in channelsV1.Channels)
                if (channel == channelV1.Name)
                    foreach (var subscriberV1 in channelV1.Subscribes)
                        if (subscriberV1.Enabled)
                            if (subscriberV1.SubscriberPostUrl.Length > 0)
                                collection.Add(subscriberV1.SubscriberPostUrl);

            foreach (var internalUrl in internalUrls)
                if (internalUrl.Length > 0)
                    collection.Add(internalUrl);

            return collection.ToArray();
        }


        #region Private Classes
        private static class ChannelBadNasFileInfo {
            private static readonly BadNasFileInfo BadNasFileInfo = new BadNasFileInfo(new FileInfo(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase), "Channels.json")));

            private static readonly object SyncLock = new object();

            public static void Write(Models.ChannelsV1 channelsV1) {
                var channelsV1Json = JsonConvert.SerializeObject(channelsV1);
                lock (SyncLock) {
                    BadNasFileInfo.WriteAllText(channelsV1Json);
                }
            }

            public static Models.ChannelsV1 Read() {
                lock (SyncLock) {
                    if (BadNasFileInfo.Exists()) {
                        var channelsV1Json = BadNasFileInfo.ReadAllText();
                        var channelsV1 = (Models.ChannelsV1)JsonConvert.DeserializeObject<Models.ChannelsV1>(channelsV1Json);
                        return channelsV1;
                    }
                    return new Models.ChannelsV1();
                }
            }
        }
        #endregion
    }
}
