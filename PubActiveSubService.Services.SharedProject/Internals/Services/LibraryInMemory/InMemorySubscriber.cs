using System.Collections.Generic;

namespace PubActiveSubService.Internals.Services.LibraryInMemory {
    public class InMemorySubscriber : Models.Subscriber {
        public readonly Queue<Models.Package> PackageQueue = new Queue<Models.Package>();
    }
}
