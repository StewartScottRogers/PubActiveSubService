using System.Collections.Generic;
using System.Linq;

namespace PubActiveSubService.Internals.Services.Library {
    public static class ChannelSearchExtentions {
        public static IEnumerable<Models.Channel> Search(this IEnumerable<Models.Channel> Channels, string search) {
            search = search.ToEnforceChannelSearchNamingConventions();

            if (search.Length <= 0) {
                foreach (var channel in Channels)
                    yield return channel;
                yield break;
            }

            if (search.EndsWith("*")) {
                search = search.TrimEnd('*');
                foreach (var channel in Channels)
                    if (channel.ChannelName.StartsWith(search))
                        yield return channel;
                yield break;
            }

            foreach (var channel in Channels)
                if (channel.ChannelName.Equals(search))
                    yield return channel;
            yield break;
        }

        public static IEnumerable<Models.Channel> Lookup(this IEnumerable<Models.Channel> Channels, string channelName) {
            if (channelName.Trim().EndsWith("*"))
                yield break;

            foreach (var channel in Channels.Where(channel => channel.ChannelName.Equals(channelName.ToEnforcedChannelNamingConventions())))
                yield return channel;
        }
    }
}
