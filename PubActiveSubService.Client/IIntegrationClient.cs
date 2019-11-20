using System;
using System.Collections.Generic;

namespace PubActiveSubService {
    public interface IIntegrationClient {
        string Ping(Uri uri);
        string PingThrough(Uri uri, string urlTarget);

        IEnumerable<Models.TracedChannel> TraceChannels(Uri uri, Models.ChannelSearch channelSearch);
        IEnumerable<Models.ListedChannel> ListChannels(Uri uri, Models.ChannelSearch channelSearch);

        void Subscribe(Uri uri, Models.Subscribe subscribe);
        void Unsubscribe(Uri uri, Models.Unsubscribe unsubscribe);

        string Publish(Uri uri, Models.PublishPackage publishPackage);

        string PublishLoopback(Uri uri, Models.PublishPackage publishPackage);
    }
}
