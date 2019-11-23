using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace PubActiveSubService.Internals.Services.Library {
    public static class DiagnosticChannelBuilder {
        public static Models.Subscribe[] GetBuiltInSubscribers(string testChannelConfiguration) {
            testChannelConfiguration = testChannelConfiguration.Trim();
            if (testChannelConfiguration.Length <= 0)
                return new Models.Subscribe[] { };

            var modelSubscribes = new Collection<Models.Subscribe>();

            var channelParts = testChannelConfiguration.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var channelPart in channelParts) {

                var subscriberParts = channelPart.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                if (subscriberParts.Length >= 1)
                    foreach (var subscriberPart in subscriberParts.Skip(1).ToArray()) {
                        var modelSubscriber = new Models.Subscribe();
                        modelSubscriber.ChannelName = subscriberParts[0];
                        modelSubscriber.SubscriberName = subscriberPart.Trim();
                        modelSubscriber.Enabled = true;
                        modelSubscribes.Add(modelSubscriber);
                    }

            }
            var results = modelSubscribes.ToArray();
            return results;
        }
    }
}
