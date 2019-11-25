using PubActiveSubService.Models;

namespace PubActiveSubService.Internals.Interfaces {
    public interface IPublisherClient {
        Results Get(string url);

        Results[] Post<T>(T t, params string[] urls) where T : class;

        Results Post<T>(T t, string url) where T : class;
    }
}
