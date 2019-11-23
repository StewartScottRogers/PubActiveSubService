using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;

namespace PubActiveSubService.Internals.Services.LibraryInMemory {
    public static class InMemoryDatabase {
        public static readonly ReaderWriterLockSlim ReaderWriterLockSlim = new ReaderWriterLockSlim();

        public static readonly ICollection<InMemoryChannel> InMemoryChannels = new Collection<InMemoryChannel>();
    }
}
