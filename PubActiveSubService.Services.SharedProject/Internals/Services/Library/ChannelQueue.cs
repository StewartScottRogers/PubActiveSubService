using System.Collections.Generic;

namespace PubActiveSubService.Internals.Services.Library {
    public class ChannelQueue : Models.Channel {
        public readonly Queue<Models.PublishPackage> PublishPackageQueue = new Queue<Models.PublishPackage>();
    }
}
