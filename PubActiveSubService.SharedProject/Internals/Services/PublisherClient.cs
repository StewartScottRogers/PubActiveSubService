using Newtonsoft.Json;
using PubActiveSubService.Internals.Interfaces;
using System.Net.Http;
using System.Text;

namespace PubActiveSubService.Internals.Services {
    public class PublisherClient : IPublisherClient {
        public string Get(string url) {
            var stringBuilder = new StringBuilder();
            using (var httpClient = new HttpClient()) {
                try {
                    var httpResponseMessage = httpClient.GetAsync(url).Result;
                    httpResponseMessage.EnsureSuccessStatusCode();
                    var responseBody = httpResponseMessage.Content.ReadAsStringAsync().Result;
                    stringBuilder.AppendLine(responseBody);
                } catch (System.Exception exception) {
                    stringBuilder.AppendLine($"Message :{exception.Message}");
                }
            }
            return stringBuilder.ToString();
        }

        public string Post<T>(T t, params string[] urls) where T : class {
            var stringBuilder = new StringBuilder();
            foreach (var url in urls)
                using (var httpClient = new HttpClient()) {
                    try {
                        var contentStyring = JsonConvert.SerializeObject(t);
                        var httpContent = new StringContent(contentStyring, Encoding.UTF8, "application/json");
                        var httpResponseMessage = httpClient.PostAsync(url, httpContent).Result;
                        httpResponseMessage.EnsureSuccessStatusCode();
                        var responseBody = httpResponseMessage.Content.ReadAsStringAsync().Result;
                        stringBuilder.AppendLine(responseBody);
                    } catch (System.Exception exception) {
                        stringBuilder.AppendLine($"Message :{exception.Message}");
                    }
                }
            return stringBuilder.ToString();
        }
    }
}
