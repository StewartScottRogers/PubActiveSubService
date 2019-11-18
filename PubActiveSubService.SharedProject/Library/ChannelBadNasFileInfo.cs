using Newtonsoft.Json;

using System;
using System.IO;
using System.Reflection;

namespace PubActiveSubService.Library {
    public static class ChannelBadNasFileInfo {
        private static readonly BadNasFileInfo BadNasFileInfo = null;
        private static readonly object SyncLock = new object();
        private static Models.ChannelsV1 ChannelsV1 = null;

        static ChannelBadNasFileInfo() {
            var directoryPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var filePath = Path.Combine(directoryPath, "Channels.json");
            var fileInfo = new FileInfo(filePath);
            BadNasFileInfo = new BadNasFileInfo(fileInfo);
        }

        public static void Write(Models.ChannelsV1 channelsV1) {
            try {
                lock (SyncLock) {
                    ChannelsV1 = channelsV1.DeepClone();
                    BadNasFileInfo.WriteAllText(JsonConvert.SerializeObject(ChannelsV1));
                }
            } catch (Exception exception) {
                throw exception;
            }
        }

        public static Models.ChannelsV1 Read() {
            try {
                lock (SyncLock) {
                    if (null != ChannelsV1)
                        return ChannelsV1.DeepClone(); ;

                    if (BadNasFileInfo.Exists()) {
                        var channelsV1Json = BadNasFileInfo.ReadAllText();
                        var channelsV1 = (Models.ChannelsV1)JsonConvert.DeserializeObject<Models.ChannelsV1>(channelsV1Json);
                        return channelsV1;
                    }
                    return new Models.ChannelsV1();
                }
            } catch (Exception exception) {
                throw exception;
            }
        }
    }
}
