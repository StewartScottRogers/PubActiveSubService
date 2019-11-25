using System;
using System.Collections.Generic;

namespace PubActiveSubService {
    public interface IIntegrationClient {
        string Touch(Uri uri);
        string TouchThrough(Uri uri, string urlTarget);

        IEnumerable<Models.ChannelStatus> TraceChannels(Uri uri, Models.ChannelSearch channelSearch);
        IEnumerable<Models.Channel> ListChannels(Uri uri, Models.ChannelSearch channelSearch);

        void Subscribe(Uri uri, Models.Subscribe subscribe);
        void Unsubscribe(Uri uri, Models.Unsubscribe unsubscribe);

        string Publish(Uri uri, Models.Package package);

        string PublishLoopback(Uri uri, Models.Package package);
    }
}
