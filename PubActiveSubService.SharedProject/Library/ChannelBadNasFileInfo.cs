using Newtonsoft.Json;
using System.IO;
using System.Reflection;

namespace PubActiveSubService.Library {
    public static class ChannelBadNasFileInfo {
        private static readonly BadNasFileInfo BadNasFileInfo = new BadNasFileInfo(new FileInfo(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase), "Channels.json")));
        private static readonly object SyncLock = new object();
        private static Models.ChannelsV1 ChannelsV1 = null;

        public static void Write(Models.ChannelsV1 channelsV1) {
            lock (SyncLock) {
                ChannelsV1 = channelsV1.DeepClone();
                BadNasFileInfo.WriteAllText(JsonConvert.SerializeObject(ChannelsV1));
            }
        }

        public static Models.ChannelsV1 Read() {
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
        }
    }
}
