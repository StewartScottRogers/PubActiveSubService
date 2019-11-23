using System.Collections.Generic;
using System.Linq;

namespace PubActiveSubService.Internals.Services.LibraryInMemory {
    public static class InMemoryChannelsExtentions {
        public static IEnumerable<InMemoryChannel> Search(this IEnumerable<InMemoryChannel> InMemoryChannels, string search) {
            search = search.ToEnforceChannelSearchNamingConventions();

            if (search.Length <= 0) {
                foreach (var InMemoryChannel in InMemoryChannels)
                    yield return InMemoryChannel;
                yield break;
            }

            if (search.EndsWith("*")) {
                search = search.TrimEnd('*');
                foreach (var InMemoryChannel in InMemoryChannels)
                    if (InMemoryChannel.ChannelName.StartsWith(search))
                        yield return InMemoryChannel;
                yield break;
            }

            foreach (var InMemoryChannel in InMemoryChannels)
                if (InMemoryChannel.ChannelName.Equals(search))
                    yield return InMemoryChannel;
            yield break;
        }

        public static IEnumerable<InMemoryChannel> Lookup(this IEnumerable<InMemoryChannel> InMemoryChannels, string channelName) {
            if (channelName.Trim().EndsWith("*"))
                yield break;

            foreach (var inMemoryChannel in InMemoryChannels.Where(channel => channel.ChannelName.Equals(channelName.ToEnforcedChannelNamingConventions())))
                yield return inMemoryChannel;
        }
    }
}
