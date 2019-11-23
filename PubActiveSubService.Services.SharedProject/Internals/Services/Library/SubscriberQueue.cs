using System.Collections.Generic;

namespace PubActiveSubService.Internals.Services.Library {
    public class SubscriberQueue : Models.Subscriber {
        public readonly Queue<Models.PublishPackage> PublishPackageQueue = new Queue<Models.PublishPackage>();
    }
}
