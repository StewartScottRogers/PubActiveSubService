using PubActiveSubService.Internals.Services.Library;
using System.Collections.Generic;

namespace PubActiveSubService.Internals.Services.LibraryInMemory {
    public class InMemoryChannel  {
        public string ChannelName { get; set; } = string.Empty;

        public List<InMemorySubscriber> InMemorySubscribers { get; set; } = new List<InMemorySubscriber>();
    }
}
