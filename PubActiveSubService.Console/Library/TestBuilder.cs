using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Library {
    public static class TestBuilder {
        private const string TestSubscribeDefinition =
                                                        @"test ^t0|http://localhost/api/loopbackpost ^t1^t2^t3 > " +
                                                        @"test/alpha ^a0^a1^a2^a3 > " +
                                                        @"test/beta ^b0^b1^b2^b3| > " +
                                                        @"test/charle ^c0^c1^c2^c3 > " +
                                                        @"test/delta ^d0^d1^d2^d3 > " +
                                                        @"test/echo ^e0^e1^e2^e3"
                                                     ;

        public static PubActiveSubService.Models.Subscribe[] GetTestSubscribes()
            => TestSubscribeDefinition.ToSubscribes();

        private static PubActiveSubService.Models.Subscribe[] ToSubscribes(this string testChannelAndSubscribeDefinition) {
            testChannelAndSubscribeDefinition = testChannelAndSubscribeDefinition.Trim();
            if (testChannelAndSubscribeDefinition.Length <= 0)
                return new PubActiveSubService.Models.Subscribe[] { };

            var modelSubscribes = new Collection<PubActiveSubService.Models.Subscribe>();

            var channelParts = testChannelAndSubscribeDefinition.Split(new char[] { '>' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var channelPart in channelParts) {

                var subscriberParts = channelPart.Split(new char[] { '^' }, StringSplitOptions.RemoveEmptyEntries);
                if (subscriberParts.Length >= 1)
                    foreach (var subscriberPart in subscriberParts.Skip(1).ToArray()) {
                        var subscriberAndMaybeUrl = subscriberPart.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                        var channelName = subscriberParts[0].Trim();
                        var subscriberName = subscriberAndMaybeUrl[0].Trim();
                        var subscriberPostUrl = string.Empty;
                        if (subscriberAndMaybeUrl.Length > 1)
                            subscriberPostUrl = subscriberAndMaybeUrl[1].Trim();

                        var modelSubscriber = new PubActiveSubService.Models.Subscribe();
                        modelSubscriber.ChannelName = channelName;
                        modelSubscriber.SubscriberName = subscriberName;
                        modelSubscriber.SubscriberPostUrl = subscriberPostUrl;
                        modelSubscriber.Enabled = (subscriberPostUrl.Length > 0);
                        modelSubscribes.Add(modelSubscriber);
                    }

            }
            var results = modelSubscribes.ToArray();
            return results;
        }
    }
}
