using PubActiveSubService.Models;
using System.Collections.Generic;

namespace PubActiveSubService {
    public interface IIntegrationProcessors {

        string PopulateTestData(string hostUrl);

        void SaveHostUrl(string hostUrl);

        string Touch();
        Results TouchThrough(string url);

        IEnumerable<Models.ChannelStatus> TraceChannels(Models.Search search);
        IEnumerable<Models.Channel> ListChannels(Models.Search search);

        void Subscribe(Models.Subscribe subscribe);
        void Unsubscribe(Models.Unsubscribe unsubscribe);

        Package[] Publish(Models.Package package);

        Package[] PublishLoopback(Models.Package package);
    }
}
