using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PubActiveSubService.Internals.Services.Library {
    public static class QueuedChannelCollectionManager {
        public static readonly ICollection<ChannelQueue> QueuedChannelCollection = new Collection<ChannelQueue>();
       
    }
}
