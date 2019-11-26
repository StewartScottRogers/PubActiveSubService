using Newtonsoft.Json;

using PubActiveSubService.Internals.Interfaces;
using PubActiveSubService.Models;

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PubActiveSubService.Internals.Services {
    public class PublisherClient : IPublisherClient {

        public Results Get(string url) {
            ConfirmVWellFormedUriString(url);

            return GetExternal(url);
        }

        public Results[] Post<T>(T t, params string[] urls) where T : class {
            ConfirmVWellFormedUriString(urls);

            var results = new Collection<Results>();
            Parallel.ForEach(
                urls, (Action<string>)(
                                            (url) => {
                                                var postResults = PostExternal(t, url);
                                                lock (results) {
                                                    results.Add(postResults);
                                                }
                                            }
                                      )
            );
            return results.ToArray();
        }

        public Results Post<T>(T t, string url) where T : class {
            ;
            ConfirmVWellFormedUriString(url);

            return PostExternal(t, url);
        }

        #region Private Methods
        private static void ConfirmVWellFormedUriString(params string[] urls) {
            foreach (var url in urls) {
                System.Diagnostics.Debug.WriteLine(url);
                if (!Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
                    throw new Exception($"The Url is not Well Formed '{url}'.");
            }
        }


        private static Results PostExternal<T>(T t, string url) where T : class {
            using (var httpClient = new HttpClient()) {
                try {
                    var contentStyring = JsonConvert.SerializeObject(t);
                    var httpContent = new StringContent(contentStyring, Encoding.UTF8, "application/json");
                    var httpResponseMessage = httpClient.PostAsync(url, httpContent).Result;
                    httpResponseMessage.EnsureSuccessStatusCode();
                    var responseBody = httpResponseMessage.Content.ReadAsStringAsync().Result;
                    return new Results() { Result = responseBody, Success = true };
                } catch (System.Exception exception) {
                    var results = new Results() { Result = $"{url}, Message :{exception.Message}", Success = false };
                    System.Diagnostics.Debug.WriteLine(results.ToString());
                    return results;
                }
            }
        }

        private static Results GetExternal(string url) {
            using (var httpClient = new HttpClient()) {
                try {
                    var httpResponseMessage = httpClient.GetAsync(url).Result;
                    httpResponseMessage.EnsureSuccessStatusCode();
                    var responseBody = httpResponseMessage.Content.ReadAsStringAsync().Result;
                    return new Results() { Result = responseBody, Success = true };
                } catch (System.Exception exception) {
                    var results = new Results() { Result = $"{url}, Message :{exception.Message}", Success = false };
                    System.Diagnostics.Debug.WriteLine(results.ToString());
                    return results;
                }
            }
        }
        #endregion
    }
}
