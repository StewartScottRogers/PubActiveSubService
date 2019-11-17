using System.Collections.Generic;

namespace PubActiveSubService {
    public interface IPubActiveSubServiceProcessors {
        string Ping();
        string Pingthrough(string url);

        IEnumerable<Models.TracedChannelV1> Trace(Models.TraceChannelsV1 traceChannelsV1);
        IEnumerable<Models.ListedChannelV1> ListChanerls(Models.ListChannelsV1 listChannelsV1);

        void Subscribe(Models.SubscribeV1 subscribeV1);
        void Unsubscribe(Models.UnsubscribeV1 unsubscribeV1);

        void Publish(Models.PublishPackageV1 publishPackageV1);
    }
}
