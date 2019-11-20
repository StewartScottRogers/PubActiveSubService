using PubActiveSubService.Internals.Interfaces;
using PubActiveSubService.Models;

using System;
using System.Collections.Generic;

namespace PubActiveSubService {
    public class PubActiveSubServiceProcessors : IPubActiveSubServiceProcessors {
        private readonly IPublisherClient PublisherClient;
        private readonly IQueuePersisitance QueuePersisitance;
        private readonly IChannelPersisitance ChannelPersisitance;

        private string HostUrl = string.Empty;

        public PubActiveSubServiceProcessors(IPublisherClient publisherClient, IQueuePersisitance queuePersisitance, IChannelPersisitance channelPersisitance) {
            if (null == publisherClient) throw new ArgumentNullException(nameof(publisherClient));
            if (null == queuePersisitance) throw new ArgumentNullException(nameof(queuePersisitance));
            if (null == channelPersisitance) throw new ArgumentNullException(nameof(channelPersisitance));

            PublisherClient = publisherClient;
            QueuePersisitance = queuePersisitance;
            ChannelPersisitance = channelPersisitance;
        }

        public void SaveHostUrl(string hostUrl) => HostUrl = hostUrl;

        public string Ping() => DateTimeOffset.UtcNow.ToString();

        public string Pingthrough(string url) => PublisherClient.Get(url);


        public IEnumerable<TracedChannel> TraceChannels(ChannelSearch channelSearch) {  


            yield return new TracedChannel() { ChannelName = "Channel/One", Subscribers = new SubscriberStatus[] { new SubscriberStatus() { SubscriberName = "SubscriberOne", Status = new Status[] { new Status() { Name = "Connected.", Value = "OK" } } } } };       
        }

        public IEnumerable<ListedChannel> ListChannels(ChannelSearch channelSearch) => ChannelPersisitance.ListChannels(channelSearch);


        public void Subscribe(Subscribe subscribe) => ChannelPersisitance.Subscribe(subscribe, $"{HostUrl}/api/PublishLoopback");

        public void Unsubscribe(Unsubscribe unsubscribe) => ChannelPersisitance.Unsubscribe(unsubscribe);


        public string Publish(PublishPackage publishPackage) {
            ChannelPersisitance.PostChannelName(publishPackage.ChannelName);

            if (publishPackage.Package.Length > 0)
                return PublisherClient.Post(
                                              publishPackage,
                                              ChannelPersisitance.LookupSubscriberUrlsByChanneNamel(
                                                                                                      publishPackage.ChannelName,
                                                                                                      $"{HostUrl}/api/PublishLoopback"
                                          )

                );

            return "Empty Package! Nothing to publish.";
        }

        public string PublishLoopback(PublishPackage publishPackage) {
            return "";
        }
    }
}
