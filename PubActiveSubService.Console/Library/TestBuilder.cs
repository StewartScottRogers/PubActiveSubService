using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Library {
    public static class TestBuilder {
        private const string TestChannelAndSubscribeDefinition = @"test:t0:t1:t2:t3, test/alpha:a0:a1:a2:a3, test/beta:b0:b1:b2:b3, test/charle:c0:c1:c2:c3, test/delta:d0:d1:d2:d3, test/echo:e0:e1:e2:e3";

        public static PubActiveSubService.Models.Subscribe[]  GetTestChannelsAndSubscribers() 
            => TestChannelAndSubscribeDefinition.GetChannelsAndSubscribers();
    
        private static PubActiveSubService.Models.Subscribe[] GetChannelsAndSubscribers(this string testChannelAndSubscribeDefinition) {
            testChannelAndSubscribeDefinition = testChannelAndSubscribeDefinition.Trim();
            if (testChannelAndSubscribeDefinition.Length <= 0)
                return new PubActiveSubService.Models.Subscribe[] { };

            var modelSubscribes = new Collection<PubActiveSubService.Models.Subscribe>();

            var channelParts = testChannelAndSubscribeDefinition.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var channelPart in channelParts) {

                var subscriberParts = channelPart.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                if (subscriberParts.Length >= 1)
                    foreach (var subscriberPart in subscriberParts.Skip(1).ToArray()) {
                        var modelSubscriber = new PubActiveSubService.Models.Subscribe();
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
