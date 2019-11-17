using PubActiveSubService.Models;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace PubActiveSubService.Services {
    public class PubActiveSubServiceProcessors : IPubActiveSubServiceProcessors {
        public PubActiveSubServiceProcessors() { }

        public string Ping() => DateTimeOffset.UtcNow.ToString();

        public string Pingthrough(string url) {
            var stringBuilder = new StringBuilder();
            using (var httpClient = new HttpClient()) {
                try {
                    var httpResponseMessage = httpClient.GetAsync(url).Result;
                    httpResponseMessage.EnsureSuccessStatusCode();
                    var responseBody = httpResponseMessage.Content.ReadAsStringAsync().Result;
                    stringBuilder.AppendLine(responseBody);
                } catch (HttpRequestException httpRequestException) {
                    stringBuilder.AppendLine($"Message :{httpRequestException.Message}");
                }
            }
            return stringBuilder.ToString();
        }


        public IEnumerable<TracedChannelV1> Trace(TraceChannelsV1 traceChannelsV1) {
            yield return new TracedChannelV1() { ChannelName = "Channel/One", Subscribers = new SubscriberStatusV1[] { new SubscriberStatusV1() { Subscriber = "SubscriberOne", Status = new StatusV1[] { new StatusV1() { Name = "Connected.", Value = "OK" } } } } };
            yield return new TracedChannelV1() { ChannelName = "Channel/Two" };
            yield return new TracedChannelV1() { ChannelName = "Channel/Three" };
        }

        public IEnumerable<ListedChannelV1> ListChanerls(ListChannelsV1 listChannelsV1) {
            yield return new ListedChannelV1() { ChannelName = "Channel/One", Subscribers = new string[] { "SubscriberOne", "SubscriberTwo", "SubscriberThree" } };
            yield return new ListedChannelV1() { ChannelName = "Channel/Two", Subscribers = new string[] { "SubscriberOne", "SubscriberTwo", "SubscriberThree" } };
            yield return new ListedChannelV1() { ChannelName = "Channel/Three", Subscribers = new string[] { "SubscriberOne", "SubscriberTwo", "SubscriberThree" } };
        }


        public void Subscribe(SubscribeV1 subscribeV1) { }

        public void Unsubscribe(UnsubscribeV1 unsubscribeV1) { }


        public void Publish(PublishPackageV1 publishPackageV1) { }
    }
}
