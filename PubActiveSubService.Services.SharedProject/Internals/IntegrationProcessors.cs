using Newtonsoft.Json;
using PubActiveSubService.Internals.Interfaces;
using PubActiveSubService.Models;
using PubActiveSubService.Internals.Services.Library;

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

        public string PopulateTestData(string hostUrl) {
            var testChannelConfiguration = AppSettingsReader.GetTestChannelConfiguration();
            var modelSubscribers = DiagnosticChannelBuilder.GetBuiltInSubscribers(testChannelConfiguration);
            foreach (var modelSubscriber in modelSubscribers) {
                ChannelPersisitance.PostChannelName(modelSubscriber.ChannelName);
                ChannelPersisitance.Subscribe(modelSubscriber, $"{hostUrl}/api/PublishLoopback");
            }
            return testChannelConfiguration;
        }

        public void SaveHostUrl(string hostUrl) => HostUrl = hostUrl;

        public string Touch() => "Touched @ " + DateTimeOffset.UtcNow.ToString();

        public Results TouchThrough(string url) => PublisherClient.Get(url);


        public IEnumerable<ChannelStatus> TraceChannels(ChannelSearch channelSearch) {
            var listedChannels = ChannelPersisitance.ListChannels(channelSearch).ToArray();
            foreach (var listedChannel in listedChannels) {
                var channelStatus = new ChannelStatus();
                channelStatus.ChannelName = listedChannel.ChannelName;
                foreach (var subscriber in listedChannel.Subscribers) {
                    var subscriberStatus = new SubscriberStatus();
                    subscriberStatus.SubscriberName = subscriber.SubscriberName;
                    subscriberStatus.Status.Add(new Status() { Name = "Get", Value = PublisherClient.Get(subscriber.SubscriberPostUrl).Result });
                    subscriberStatus.Url = subscriber.SubscriberPostUrl;
                    channelStatus.SubscriberStatuses.Add(subscriberStatus);
                }
                yield return channelStatus;
            }
        }


        public IEnumerable<Channel> ListChannels(ChannelSearch channelSearch) => ChannelPersisitance.ListChannels(channelSearch);


        public void Subscribe(Subscribe subscribe) => ChannelPersisitance.Subscribe(subscribe, $"{HostUrl}/api/PublishLoopback");

        public void Unsubscribe(Unsubscribe unsubscribe) => ChannelPersisitance.Unsubscribe(unsubscribe);


        public Package[] Publish(Package package) {
            ChannelPersisitance.PostChannelName(package.ChannelName);

            if (package.Message.Length > 0) {
                var postResult = PublisherClient.Post(
                                                                    package,
                                                                    ChannelPersisitance.LookupSubscriberUrlsByChanneNamel(
                                                                                                                            package.ChannelName,
                                                                                                                            $"{HostUrl}/api/PublishLoopback"
                                                                    )
                                                            );

                return new Package[]{
                                                new Package() {
                                                                        ChannelName = package.ChannelName,
                                                                        MessageHeaders = package.MessageHeaders,
                                                                        Message = JsonConvert.SerializeObject(postResult),
                                                }
            };
            }

            return new Package[]{
                new Package() {
                        ChannelName = package.ChannelName,
                        MessageHeaders = package.MessageHeaders,
                        Message = "Empty Package! Nothing to publish."
                }
            };
        }

        public Package[] PublishLoopback(Package package) =>
            new Package[] {
                new Package() {
                                        ChannelName=package.ChannelName,
                                        MessageHeaders = new List<NameValuePair>(){ new NameValuePair() { Name = "loopbackheader", Value= Touch() } },
                                        Message = "Loopback Result Package! Nothing was published."
                }
            };
    }
}
