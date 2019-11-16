using Microsoft.Extensions.DependencyInjection;

namespace PubActiveSubService {
    public static class PubActiveSubServiceConfigurations {
        public static void AddPubActiveSubServices(this IServiceCollection services) {
            services.AddSingleton<IPubActiveSubServiceProcessors, Services.PubActiveSubServiceProcessors>();
        }
    }
}
