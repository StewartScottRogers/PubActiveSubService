using Newtonsoft.Json;

using System;
using System.IO;
using System.Reflection;

namespace PubActiveSubService.Library {
    public static class ChannelFileInfo {
        private static readonly BadNasFileInfo BadNasFileInfo = null;
        private static readonly object SyncLock = new object();
        private static Models.Channels Channels = null;

        static ChannelFileInfo() {
            var directoryPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var filePath = Path.Combine(directoryPath, "Channels.json");
            var fileInfo = new FileInfo(filePath);
            BadNasFileInfo = new BadNasFileInfo(fileInfo);
        }

        public static void Write(Models.Channels channels) {
            try {
                lock (SyncLock) {
                    Channels = channels.DeepClone();
                    BadNasFileInfo.WriteAllText(JsonConvert.SerializeObject(Channels));
                }
            } catch (Exception exception) {
                throw exception;
            }
        }

        public static Models.Channels Read() {
            try {
                lock (SyncLock) {
                    if (null != Channels)
                        return Channels.DeepClone(); 

                    if (BadNasFileInfo.Exists()) {
                        var channelsJson = BadNasFileInfo.ReadAllText();
                        var channels = (Models.Channels)JsonConvert.DeserializeObject<Models.Channels>(channelsJson);
                        return channels;
                    }
                    return new Models.Channels();
                }
            } catch (Exception exception) {
                throw exception;
            }
        }
    }
}
