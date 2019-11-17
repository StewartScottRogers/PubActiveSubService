using System;
using System.Net.Http;
using System.Text;

namespace PubActiveSubService.Services {
    public class PubActiveSubServiceProcessors : IPubActiveSubServiceProcessors {
        public PubActiveSubServiceProcessors() { }

        public string Ping() {
            return DateTimeOffset.UtcNow.ToString();
        }

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
    }
}
