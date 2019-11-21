using PubActiveSubService.Models;

namespace PubActiveSubService.Internals.Interfaces {
    public interface IPublisherClient {
        PublishResult Get(string url);

        PublishResult[] Post<T>(T t, params string[] urls) where T : class;

        PublishResult Post<T>(T t, string url) where T : class;
    }
}
