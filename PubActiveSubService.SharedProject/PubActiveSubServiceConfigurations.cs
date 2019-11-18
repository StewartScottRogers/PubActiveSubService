using Microsoft.Extensions.DependencyInjection;
using PubActiveSubService.Internals.Interfaces;
using PubActiveSubService.Internals.Services;

namespace PubActiveSubService {
    public static class PubActiveSubServiceConfigurations {
        public static void AddPubActiveSubServices(this IServiceCollection services) {
            services.AddSingleton<IPubActiveSubServiceProcessors, PubActiveSubServiceProcessors>();
            services.AddSingleton<IPublisherClient, PublisherClient>();
            services.AddSingleton<IQueuePersisitance, QueuePersisitance>();
            services.AddSingleton<IChannelPersisitance, ChannelPersisitance>();

            
        }
    }
}
