using System.Collections.Generic;

namespace PubActiveSubService {
    public interface IIntegrationProcessors {
        void SaveHostUrl(string hostUrl);

        string Ping();
        string Pingthrough(string url);

        IEnumerable<Models.TracedChannel> TraceChannels(Models.ChannelSearch channelSearch);
        IEnumerable<Models.ListedChannel> ListChannels(Models.ChannelSearch channelSearch);

        void Subscribe(Models.Subscribe subscribe);
        void Unsubscribe(Models.Unsubscribe unsubscribe);

        string Publish(Models.PublishPackage publishPackage);

        string PublishLoopback(Models.PublishPackage publishPackage);
    }
}
