using Microsoft.Extensions.DependencyInjection;
using PubActiveSubService.Internals.Interfaces;
using PubActiveSubService.Internals.Services;

namespace PubActiveSubService {
    public static class IntegrationProcessorsConfigurations {
        public static void AddIntegrationServer(this IServiceCollection services) {
            services.AddSingleton<IIntegrationProcessors, IntegrationProcessors>();

            services.AddSingleton<IAppSettingsReader, AppSettingsReader>();
            services.AddSingleton<IPublisherClient, PublisherClient>();
            //services.AddSingleton<IQueuePersisitance, QueuePersisitanceFileSystem>();
            services.AddSingleton<IQueuePersisitance, QueuePersisitanceInMemory>();
            //services.AddSingleton<IChannelPersisitance, ChannelPersisitanceFileSystem>();
            services.AddSingleton<IChannelPersisitance, ChannelPersisitanceInMemory>();
        }
    }
}
