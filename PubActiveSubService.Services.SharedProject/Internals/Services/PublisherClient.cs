using Newtonsoft.Json;
using PubActiveSubService.Internals.Interfaces;
using PubActiveSubService.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PubActiveSubService.Internals.Services {
    public class PublisherClient : IPublisherClient {
        public PublishResult Get(string url) {
            using (var httpClient = new HttpClient()) {
                try {
                    var httpResponseMessage = httpClient.GetAsync(url).Result;
                    httpResponseMessage.EnsureSuccessStatusCode();
                    var responseBody = httpResponseMessage.Content.ReadAsStringAsync().Result;
                    return new PublishResult() { Result = responseBody, Success = true };
                } catch (System.Exception exception) {
                    return new PublishResult() { Result = $"Message :{exception.Message}", Success = false };
                }
            }
        }

        public PublishResult[] Post<T>(T t, params string[] urls) where T : class {
            var results = new Collection<PublishResult>();

            Parallel.ForEach(urls, (url) => {
                lock (results) {
                    results.Add(Post(t, url));
                }
            }
            );

            return results.ToArray();
        }

        public PublishResult Post<T>(T t, string url) where T : class {
            using (var httpClient = new HttpClient()) {
                try {
                    var contentStyring = JsonConvert.SerializeObject(t);
                    var httpContent = new StringContent(contentStyring, Encoding.UTF8, "application/json");
                    var httpResponseMessage = httpClient.PostAsync(url, httpContent).Result;
                    httpResponseMessage.EnsureSuccessStatusCode();
                    var responseBody = httpResponseMessage.Content.ReadAsStringAsync().Result;
                    return new PublishResult() { Result = responseBody, Success = true };
                } catch (System.Exception exception) {
                    return new PublishResult() { Result = $"Message :{exception.Message}", Success = false };
                }
            }
        }
    }
}
