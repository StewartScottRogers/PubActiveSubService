using Microsoft.AspNetCore.Mvc;

using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;

namespace PubActiveSubService.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class PingThroughV1Controller : ControllerBase {
        // POST: api/PingThroughV1
        [HttpPost]
        public string Post([FromBody] Models.PingThroughV1 pingThroughV1) {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(Pingthrough(pingThroughV1.Url));

            return stringBuilder.ToString();
        }

        private static string Pingthrough(string url) {
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

        //private static string Ping(string dns) {
        //    var stringBuilder = new StringBuilder();

        //    var ping = new Ping();
        //    var options = new PingOptions { DontFragment = true };

        //    var bytes = Encoding.ASCII.GetBytes(nameof(PingThroughV1Controller));
        //    var timeout = 1024 * 10;

        //    stringBuilder.AppendLine($"Pinging '{dns}'");
        //    var pingReply = ping.Send(dns, timeout, bytes, options);

        //    if (pingReply.Status == IPStatus.Success) {
        //        stringBuilder.AppendLine($"Address: {pingReply.Address}");
        //        stringBuilder.AppendLine($"RoundTrip time: {pingReply.RoundtripTime}");
        //        stringBuilder.AppendLine($"Time to live: {pingReply.Options.Ttl}");
        //        stringBuilder.AppendLine($"Don't fragment: {pingReply.Options.DontFragment}");
        //        stringBuilder.AppendLine($"Buffer size: {pingReply.Buffer.Length}");
        //    } else {
        //        stringBuilder.AppendLine(pingReply.Status.ToString());
        //    }

        //    return stringBuilder.ToString();
        //}
    }
}
