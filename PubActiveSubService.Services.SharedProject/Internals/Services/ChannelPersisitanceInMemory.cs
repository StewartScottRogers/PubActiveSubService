using PubActiveSubService.Internals.Interfaces;
using System;

namespace PubActiveSubService.Internals.Services {
    public class ChannelPersisitanceInMemory {
        private readonly IAppSettingsReader AppSettingsReader;

        public ChannelPersisitanceInMemory(IAppSettingsReader appSettingsReader) {
            if (null == appSettingsReader) throw new ArgumentNullException(nameof(appSettingsReader));

            AppSettingsReader = appSettingsReader;
        }
    }
}
