using PubActiveSubService.Internals.Interfaces;
using System.Collections.ObjectModel;
using System.Linq;

namespace PubActiveSubService.Internals.Services {
    public class SubscriberPersisitance : ISubscriberPersisitance {
        public string[] SubscriberUrls(string Channel, params string[] urls) {
            var collection = new Collection<string>();

            foreach (var url in urls)
                if (url.Length > 0)
                    collection.Add(url);


            return collection.ToArray();
        }
    }
}
