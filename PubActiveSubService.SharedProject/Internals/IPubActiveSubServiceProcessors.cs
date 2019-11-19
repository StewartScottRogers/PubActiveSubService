using System.Collections.Generic;

namespace PubActiveSubService {
    public interface IPubActiveSubServiceProcessors {
        void SaveArchiveHostDns(string url);

        string Ping();
        string Pingthrough(string url);

        IEnumerable<Models.TracedChannelV1> Trace(Models.ChannelSearchV1 channelSearchV1);
        IEnumerable<Models.ListedChannelV1> ListChannels(Models.ChannelSearchV1 channelSearchV1);

        void Subscribe(Models.SubscribeV1 subscribeV1);
        void Unsubscribe(Models.UnsubscribeV1 unsubscribeV1);

        string Publish(Models.PublishPackageV1 publishPackageV1);

        string PublishArchive(Models.PublishPackageV1 publishPackageV1);
    }
}
