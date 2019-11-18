using PubActiveSubService.Internals.Interfaces;
using PubActiveSubService.Models;

using System;
using System.Collections.Generic;

namespace PubActiveSubService {
    public class PubActiveSubServiceProcessors : IPubActiveSubServiceProcessors {
        private readonly IPublisherClient PublisherClient;
        private readonly IQueuePersisitance ChannelPersisitance;
        private readonly IChannelPersisitance PublisherPersisitance;

        private string ArchiveUrl = string.Empty;

        public PubActiveSubServiceProcessors(IPublisherClient publisherClient, IQueuePersisitance queuePersisitance, IChannelPersisitance channelPersisitance) {
            if (null == publisherClient) throw new ArgumentNullException(nameof(publisherClient));
            if (null == queuePersisitance) throw new ArgumentNullException(nameof(queuePersisitance));
            if (null == channelPersisitance) throw new ArgumentNullException(nameof(channelPersisitance));

            PublisherClient = publisherClient;
            ChannelPersisitance = queuePersisitance;
            PublisherPersisitance = channelPersisitance;
        }

        public void SaveArchiveHost(string url) => ArchiveUrl = $"{url}/api/PublishArchiveV1";

        public string Ping() => DateTimeOffset.UtcNow.ToString();

        public string Pingthrough(string url) => PublisherClient.Get(url);


        public IEnumerable<TracedChannelV1> Trace(TraceChannelsV1 traceChannelsV1) {
            yield return new TracedChannelV1() { ChannelName = "Channel/One", Subscribers = new SubscriberStatusV1[] { new SubscriberStatusV1() { Subscriber = "SubscriberOne", Status = new StatusV1[] { new StatusV1() { Name = "Connected.", Value = "OK" } } } } };
            yield return new TracedChannelV1() { ChannelName = "Channel/Two", Subscribers = new SubscriberStatusV1[] { new SubscriberStatusV1() { Subscriber = "SubscriberTwo", Status = new StatusV1[] { new StatusV1() { Name = "Connected.", Value = "OK" } } } } };
            yield return new TracedChannelV1() { ChannelName = "Channel/Three", Subscribers = new SubscriberStatusV1[] { new SubscriberStatusV1() { Subscriber = "SubscriberThree", Status = new StatusV1[] { new StatusV1() { Name = "Connected.", Value = "OK" } } } } };
        }

        public IEnumerable<ListedChannelV1> ListChanerls(ListChannelsV1 listChannelsV1) {
            yield return new ListedChannelV1() { ChannelName = "Channel/One", Subscribers = new string[] { "SubscriberOne", "SubscriberTwo", "SubscriberThree" } };
            yield return new ListedChannelV1() { ChannelName = "Channel/Two", Subscribers = new string[] { "SubscriberOne", "SubscriberTwo", "SubscriberThree" } };
            yield return new ListedChannelV1() { ChannelName = "Channel/Three", Subscribers = new string[] { "SubscriberOne", "SubscriberTwo", "SubscriberThree" } };
        }


        public void Subscribe(SubscribeV1 subscribeV1) { }

        public void Unsubscribe(UnsubscribeV1 unsubscribeV1) { }


        public string Publish(PublishPackageV1 publishPackageV1) => PublisherClient.Post(
                                                                                            publishPackageV1,
                                                                                            PublisherPersisitance.LookupSubscriberUrlsByChannel(
                                                                                                                                                    publishPackageV1.Channel,
                                                                                                                                                    ArchiveUrl
                                                                                            )
        );

        public string PublishArchive(PublishPackageV1 publishPackageV1) {
            return "";
        }
    }

}
