using Microsoft.Extensions.Configuration;

using PubActiveSubService.Internals.Interfaces;

using System.IO;

namespace PubActiveSubService.Internals.Services {
    public class AppSettingsReader : IAppSettingsReader {
        private static readonly IConfigurationBuilder ConfigurationBuilder = InitalizeAppSettingsReader();

        private static IConfigurationBuilder InitalizeAppSettingsReader() {
            return new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        }

        public string GetTestChannelConfiguration() { return ConfigurationBuilder.Build()["TestChannelConfiguration"]; }
    }
}
