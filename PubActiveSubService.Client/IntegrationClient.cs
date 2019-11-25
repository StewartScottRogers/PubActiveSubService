using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace PubActiveSubService {
    public class IntegrationClient : IIntegrationClient {

        public string Touch(Uri uri)
         => PublisherClient.Get(uri);

        public string TouchThrough(Uri uri, string urlTarget)
            => PublisherClient.Post(urlTarget, uri);

        public IEnumerable<Models.ChannelStatus> TraceChannels(Uri uri, Models.Search search)
            => PublisherClient.Post<IEnumerable<Models.ChannelStatus>, Models.Search>(search, uri);

        public string Publish(Uri uri, Models.Package package)
            => PublisherClient.Post(package, uri);

        public string PublishLoopback(Uri uri, Models.Package package)
           => PublisherClient.Post(package, uri);

        public IEnumerable<Models.Channel> ListChannels(Uri uri, Models.Search search)
           => PublisherClient.Post<IEnumerable<Models.Channel>, Models.Search>(search, uri);

        public void Subscribe(Uri uri, Models.Subscribe subscribe)
            => PublisherClient.Post(subscribe, uri);

        public void Unsubscribe(Uri uri, Models.SubscriberBinding subscriberBinding)
            => PublisherClient.Post(subscriberBinding, uri);

        #region Private Classes
        private static class PublisherClient {
            public static string Get(Uri uri) {
                var stringBuilder = new StringBuilder();
                using (var httpClient = new HttpClient()) {
                    try {
                        var httpResponseMessage = httpClient.GetAsync(uri.OriginalString).Result;
                        httpResponseMessage.EnsureSuccessStatusCode();
                        var responseBody = httpResponseMessage.Content.ReadAsStringAsync().Result;
                        stringBuilder.AppendLine(responseBody);
                    } catch (System.Exception exception) {
                        stringBuilder.AppendLine($"Message :{exception.Message}");
                    }
                }
                return stringBuilder.ToString();
            }

            public static string Post<P>(P p, params Uri[] uris) where P : class {
                var stringBuilder = new StringBuilder();
                foreach (var uri in uris)
                    using (var httpClient = new HttpClient()) {
                        try {
                            var contentStyring = JsonConvert.SerializeObject(p);
                            var httpContent = new StringContent(contentStyring, Encoding.UTF8, "application/json");
                            var httpResponseMessage = httpClient.PostAsync(uri.OriginalString, httpContent).Result;
                            httpResponseMessage.EnsureSuccessStatusCode();
                            var responseBody = httpResponseMessage.Content.ReadAsStringAsync().Result;
                            stringBuilder.AppendLine(responseBody);
                        } catch (System.Exception exception) {
                            stringBuilder.AppendLine($"Message :{exception.Message}");
                        }
                    }
                return stringBuilder.ToString();
            }

            public static R Post<R, P>(P p, Uri uri) where P : class {
                using (var httpClient = new HttpClient()) {
                    var contentStyring = JsonConvert.SerializeObject(p);
                    var httpContent = new StringContent(contentStyring, Encoding.UTF8, "application/json");
                    var httpResponseMessage = httpClient.PostAsync(uri.OriginalString, httpContent).Result;
                    httpResponseMessage.EnsureSuccessStatusCode();
                    var responseBody = httpResponseMessage.Content.ReadAsStringAsync().Result;
                    var r = (R)JsonConvert.DeserializeObject(responseBody);
                    return r;
                }
            }

            public static IEnumerable<R> Post<R, P>(P p, params Uri[] uris) where P : class {
                foreach (var uri in uris)
                    yield return Post<R, P>(p, uri);
            }
        }
        #endregion
    }
}
