namespace PubActiveSubService.Internals.Interfaces {
    public interface IPublisherClient {
        string Get(string url);
        string Post<T>(T t, params string[] urls) where T : class;
    }
}
