using Newtonsoft.Json;

using System;
using System.IO;
using System.Reflection;

namespace PubActiveSubService.Library {
    public static class ChannelFileSystem {
        private static readonly BadNasFileInfo BadNasFileInfo = null;
        private static readonly object SyncLock = new object();
        private static ChannelsFileSystem ChannelsFileSystem = null;

        static ChannelFileSystem() {
            var directoryPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var filePath = Path.Combine(directoryPath, "Channels.json");
            var fileInfo = new FileInfo(filePath);
            BadNasFileInfo = new BadNasFileInfo(fileInfo);
        }

        public static void Write(ChannelsFileSystem channelsFileSystem) {
            try {
                lock (SyncLock) {
                    ChannelsFileSystem = channelsFileSystem.DeepClone();
                    BadNasFileInfo.WriteAllText(JsonConvert.SerializeObject(ChannelsFileSystem));
                }
            } catch (Exception exception) {
                throw exception;
            }
        }

        public static ChannelsFileSystem Read() {
            try {
                lock (SyncLock) {
                    if (null != ChannelsFileSystem)
                        return ChannelsFileSystem.DeepClone();

                    if (BadNasFileInfo.Exists()) {
                        var channelsJson = BadNasFileInfo.ReadAllText();
                        var channels = (ChannelsFileSystem)JsonConvert.DeserializeObject<ChannelsFileSystem>(channelsJson);
                        return channels;
                    }
                    return new ChannelsFileSystem();
                }
            } catch (Exception exception) {
                throw exception;
            }
        }
    }
}
