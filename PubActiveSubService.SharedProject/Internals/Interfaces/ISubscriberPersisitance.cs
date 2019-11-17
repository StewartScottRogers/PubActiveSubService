namespace PubActiveSubService.Internals.Interfaces {
    public interface ISubscriberPersisitance {
        string[] SubscriberUrls(string Channel, params string[] urls);
    }
}
