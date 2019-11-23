using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;

namespace PubActiveSubService.Internals.Services.Library {
    public static class InMemoryChannels {
        public static readonly ReaderWriterLockSlim ReaderWriterLockSlim = new ReaderWriterLockSlim();
        public static readonly ICollection<ChannelMemmory> ChannelMemmoryCollection = new Collection<ChannelMemmory>();
    }
}
