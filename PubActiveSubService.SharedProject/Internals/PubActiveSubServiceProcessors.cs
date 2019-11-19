using PubActiveSubService.Internals.Interfaces;
using PubActiveSubService.Models;

using System;
using System.Collections.Generic;

namespace PubActiveSubService {
    public class PubActiveSubServiceProcessors : IPubActiveSubServiceProcessors {
        private readonly IPublisherClient PublisherClient;
        private readonly IQueuePersisitance QueuePersisitance;
        private readonly IChannelPersisitance ChannelPersisitance;

        private string ArchiveUrl = string.Empty;

        public PubActiveSubServiceProcessors(IPublisherClient publisherClient, IQueuePersisitance queuePersisitance, IChannelPersisitance channelPersisitance) {
            if (null == publisherClient) throw new ArgumentNullException(nameof(publisherClient));
            if (null == queuePersisitance) throw new ArgumentNullException(nameof(queuePersisitance));
            if (null == channelPersisitance) throw new ArgumentNullException(nameof(channelPersisitance));

            PublisherClient = publisherClient;
            QueuePersisitance = queuePersisitance;
            ChannelPersisitance = channelPersisitance;
        }

        public void SaveArchiveHostDns(string url) => ArchiveUrl = $"{url}/api/PublishArchiveV1";

        public string Ping() => DateTimeOffset.UtcNow.ToString();

        public string Pingthrough(string url) => PublisherClient.Get(url);


        public IEnumerable<TracedChannelV1> Trace(SearchV1 searchV1) {


            yield return new TracedChannelV1() { ChannelName = "Channel/One", Subscribers = new SubscriberStatusV1[] { new SubscriberStatusV1() { SubscriberName = "SubscriberOne", Status = new StatusV1[] { new StatusV1() { Name = "Connected.", Value = "OK" } } } } };
            yield return new TracedChannelV1() { ChannelName = "Channel/Two", Subscribers = new SubscriberStatusV1[] { new SubscriberStatusV1() { SubscriberName = "SubscriberTwo", Status = new StatusV1[] { new StatusV1() { Name = "Connected.", Value = "OK" } } } } };
            yield return new TracedChannelV1() { ChannelName = "Channel/Three", Subscribers = new SubscriberStatusV1[] { new SubscriberStatusV1() { SubscriberName = "SubscriberThree", Status = new StatusV1[] { new StatusV1() { Name = "Connected.", Value = "OK" } } } } };
        }

        public IEnumerable<ListedChannelV1> ListChannels(SearchV1 searchV1) => ChannelPersisitance.ListChannels(searchV1);


        public void Subscribe(SubscribeV1 subscribeV1) => ChannelPersisitance.Subscribe(subscribeV1);

        public void Unsubscribe(UnsubscribeV1 unsubscribeV1) => ChannelPersisitance.Unsubscribe(unsubscribeV1);


        public string Publish(PublishPackageV1 publishPackageV1) {
            ChannelPersisitance.PostChannelName(publishPackageV1.ChannelName);

            if (publishPackageV1.Package.Length > 0)
                return PublisherClient.Post(
                                              publishPackageV1,
                                              ChannelPersisitance.LookupSubscriberUrlsByChanneNamel(
                                                                                                      publishPackageV1.ChannelName,
                                                                                                      ArchiveUrl
                                          )

                );

            return "Empty Package! Nothing to publish.";
        }

        public string PublishArchive(PublishPackageV1 publishPackageV1) {
            return "";
        }
    }
}
