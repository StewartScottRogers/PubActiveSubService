using Newtonsoft.Json;
using PubActiveSubService.Internals.Interfaces;
using PubActiveSubService.Models;

using System;
using System.Collections.Generic;
using System.Linq;

namespace PubActiveSubService {
    public class IntegrationProcessors : IIntegrationProcessors {
        private readonly IPublisherClient PublisherClient;
        private readonly IQueuePersisitance QueuePersisitance;
        private readonly IChannelPersisitance ChannelPersisitance;
        private readonly IAppSettingsReader AppSettingsReader;

        private string HostUrl = string.Empty;

        public IntegrationProcessors(IPublisherClient publisherClient, IQueuePersisitance queuePersisitance, IChannelPersisitance channelPersisitance, IAppSettingsReader appSettingsReader) {
            if (null == publisherClient) throw new ArgumentNullException(nameof(publisherClient));
            if (null == queuePersisitance) throw new ArgumentNullException(nameof(queuePersisitance));
            if (null == channelPersisitance) throw new ArgumentNullException(nameof(channelPersisitance));
            if (null == appSettingsReader) throw new ArgumentNullException(nameof(appSettingsReader));

            PublisherClient = publisherClient;
            QueuePersisitance = queuePersisitance;
            ChannelPersisitance = channelPersisitance;
            AppSettingsReader = appSettingsReader;
        }

        public void SaveHostUrl(string hostUrl) => HostUrl = hostUrl;

        public string Touch() => "Touched @ " + DateTimeOffset.UtcNow.ToString();

        public PublishResult TouchThrough(string url) => PublisherClient.Get(url);


        public IEnumerable<TracedChannel> TraceChannels(ChannelSearch channelSearch) {
            var listedChannels = ChannelPersisitance.ListChannels(channelSearch).ToArray();
            foreach (var listedChannel in listedChannels) {
                var tracedChannel = new TracedChannel();
                tracedChannel.ChannelName = listedChannel.ChannelName;
                foreach (var subscriber in listedChannel.Subscribers) {
                    var subscriberStatus = new SubscriberStatus();
                    subscriberStatus.SubscriberName = subscriber.SubscriberName;
                    subscriberStatus.Status.Add(new Status() { Name = "Get", Value = PublisherClient.Get(subscriber.SubscriberPostUrl).Result });
                    subscriberStatus.Url = subscriber.SubscriberPostUrl;
                    tracedChannel.Subscribers.Add(subscriberStatus);
                }
                yield return tracedChannel;
            }
        }


        public IEnumerable<ListedChannel> ListChannels(ChannelSearch channelSearch) => ChannelPersisitance.ListChannels(channelSearch);


        public void Subscribe(Subscribe subscribe) => ChannelPersisitance.Subscribe(subscribe, $"{HostUrl}/api/PublishLoopback");

        public void Unsubscribe(Unsubscribe unsubscribe) => ChannelPersisitance.Unsubscribe(unsubscribe);


        public PublishPackage[] Publish(PublishPackage publishPackage) {
            ChannelPersisitance.PostChannelName(publishPackage.ChannelName);

            if (publishPackage.Package.Length > 0) {
                var postResult = PublisherClient.Post(
                                                                    publishPackage,
                                                                    ChannelPersisitance.LookupSubscriberUrlsByChanneNamel(
                                                                                                                            publishPackage.ChannelName,
                                                                                                                            $"{HostUrl}/api/PublishLoopback"
                                                                    )
                                                            );

                return new PublishPackage[]{
                                                new PublishPackage() {
                                                                        ChannelName = publishPackage.ChannelName,
                                                                        PackageHeaders = publishPackage.PackageHeaders,
                                                                        Package = JsonConvert.SerializeObject(postResult),
                                                }
            };
            }

            return new PublishPackage[]{
                new PublishPackage() {
                        ChannelName = publishPackage.ChannelName,
                        PackageHeaders = publishPackage.PackageHeaders,
                        Package = "Empty Package! Nothing to publish."
                }
            };
        }

        public PublishPackage[] PublishLoopback(PublishPackage publishPackage) =>
            new PublishPackage[] {
                new PublishPackage() {
                                        ChannelName=publishPackage.ChannelName,
                                        PackageHeaders = new List<NameValuePair>(){ new NameValuePair() { Name = "loopbackheader", Value= Touch() } },
                                        Package = "Loopback Result Package! Nothing was published."
                }
            };
    }
}
