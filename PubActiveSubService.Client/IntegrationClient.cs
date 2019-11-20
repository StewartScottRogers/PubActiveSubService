using PubActiveSubService.Models;
using System;
using System.Collections.Generic;

namespace PubActiveSubService {
    public class IntegrationClient : IIntegrationClient {
        public IEnumerable<ListedChannel> ListChannels(Uri uri, ChannelSearch channelSearch) {
            throw new System.NotImplementedException();
        }

        public string Ping(Uri uri) {
            throw new System.NotImplementedException();
        }

        public string PingThrough(Uri uri, string urlTarget) {
            throw new System.NotImplementedException();
        }

        public string Publish(Uri uri, PublishPackage publishPackage) {
            throw new System.NotImplementedException();
        }

        public string PublishLoopback(Uri uri, PublishPackage publishPackage) {
            throw new System.NotImplementedException();
        }

        public void Subscribe(Uri uri, Subscribe subscribe) {
            throw new System.NotImplementedException();
        }

        public IEnumerable<TracedChannel> TraceChannels(Uri uri, ChannelSearch channelSearch) {
            throw new System.NotImplementedException();
        }

        public void Unsubscribe(Uri uri, Unsubscribe unsubscribe) {
            throw new System.NotImplementedException();
        }
    }
}
