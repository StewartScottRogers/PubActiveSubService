using System.Collections.Generic;

namespace PubActiveSubService.Internals.Services.LibraryInMemory {
    public class InMemorySubscriber : Models.Subscriber {
        public readonly Queue<Models.PublishPackage> PublishPackageQueue = new Queue<Models.PublishPackage>();
    }
}
