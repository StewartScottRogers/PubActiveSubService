using PubActiveSubService.Models;
using System.Collections.Generic;

namespace PubActiveSubService {
    public interface IIntegrationProcessors {

        string PopulateTestData(string hostUrl);

        void SaveHostUrl(string hostUrl);

        string Touch();
        PublishResult TouchThrough(string url);

        IEnumerable<Models.TracedChannel> TraceChannels(Models.ChannelSearch channelSearch);
        IEnumerable<Models.Channel> ListChannels(Models.ChannelSearch channelSearch);

        void Subscribe(Models.Subscribe subscribe);
        void Unsubscribe(Models.Unsubscribe unsubscribe);

        PublishPackage[] Publish(Models.PublishPackage publishPackage);

        PublishPackage[] PublishLoopback(Models.PublishPackage publishPackage);
    }
}
