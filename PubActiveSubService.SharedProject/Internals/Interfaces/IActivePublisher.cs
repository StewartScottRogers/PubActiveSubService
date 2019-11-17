namespace PubActiveSubService.Internals.Interfaces {
    public interface IActivePublisher {
        string Get(string url);
        string Post<T>(T t, params string[] urls) where T : class;
    }
}
