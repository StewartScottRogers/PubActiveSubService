using System;
using System.Collections.Generic;

namespace PubActiveSubService {
    public interface IIntegrationClient {
        string Touch(Uri uri);
        string TouchThrough(Uri uri, string urlTarget);

        IEnumerable<Models.ChannelStatus> TraceChannels(Uri uri, Models.Search search);
        IEnumerable<Models.Channel> ListChannels(Uri uri, Models.Search search);

        void Subscribe(Uri uri, Models.Subscribe subscribe);
        void Unsubscribe(Uri uri, Models.SubscriberBinding subscriberBinding);

        string Publish(Uri uri, Models.Package package);

        string PublishLoopback(Uri uri, Models.Package package);
    }
}
