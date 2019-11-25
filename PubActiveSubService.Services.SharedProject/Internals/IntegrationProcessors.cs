using Newtonsoft.Json;
using PubActiveSubService.Internals.Interfaces;
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

        public Models.Results TouchThrough(string url) => PublisherClient.Get(url);


        public IEnumerable<Models.ChannelStatus> TraceChannels(Models.Search search) {
            var listedChannels = ChannelPersisitance.ListChannels(search).ToArray();
            foreach (var listedChannel in listedChannels) {
                var channelStatus = new Models.ChannelStatus();
                channelStatus.ChannelName = listedChannel.ChannelName;
                foreach (var subscriber in listedChannel.Subscribers) {
                    var subscriberStatus = new Models.SubscriberStatus();
                    subscriberStatus.SubscriberName = subscriber.SubscriberName;
                    subscriberStatus.Status.Add(new Models.NameValuePair() { Name = "Get", Value = PublisherClient.Get(subscriber.RestUrl).Result });
                    subscriberStatus.RestUrl = subscriber.RestUrl;
                    channelStatus.SubscriberStatuses.Add(subscriberStatus);
                }
                yield return channelStatus;
            }
        }


        public IEnumerable<Models.Channel> ListChannels(Models.Search search) => ChannelPersisitance.ListChannels(search);


        public void Subscribe(Models.Subscribe subscribe) => ChannelPersisitance.Subscribe(subscribe, $"{HostUrl}/api/PublishLoopback");

        public void Unsubscribe(Models.SubscriberBinding subscriberBinding) => ChannelPersisitance.Unsubscribe(subscriberBinding);


        public Models.Package[] Publish(Models.Package package) {
            ChannelPersisitance.PostChannelName(package.ChannelName);

            if (package.Message.Length > 0) {
                var postResult = PublisherClient.Post(
                                                                    package,
                                                                    ChannelPersisitance.LookupSubscriberUrlsByChanneNamel(
                                                                                                                            package.ChannelName,
                                                                                                                            $"{HostUrl}/api/PublishLoopback"
                                                                    )
                                                            );

                return new Models.Package[]{
                                                new Models.Package() {
                                                                        ChannelName = package.ChannelName,
                                                                        MessageHeaders = package.MessageHeaders,
                                                                        Message = JsonConvert.SerializeObject(postResult),
                                                }
            };
            }

            return new Models.Package[]{
                new Models.Package() {
                        ChannelName = package.ChannelName,
                        MessageHeaders = package.MessageHeaders,
                        Message = "Empty Package! Nothing to publish."
                }
            };
        }

        public Models.Package[] PublishLoopback(Models.Package package) =>
            new Models.Package[] {
                new Models.Package() {
                                        ChannelName=package.ChannelName,
                                        MessageHeaders = new List<Models.NameValuePair>(){ new Models.NameValuePair() { Name = "loopbackheader", Value= Touch() } },
                                        Message = "Loopback Result Package! Nothing was published."
                }
            };
    }
}
