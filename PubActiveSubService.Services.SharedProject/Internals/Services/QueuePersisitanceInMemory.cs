using PubActiveSubService.Internals.Interfaces;
using System;

namespace PubActiveSubService.Internals.Services {
    public class QueuePersisitanceInMemory : IQueuePersisitance {
        private readonly IAppSettingsReader AppSettingsReader;

        public QueuePersisitanceInMemory(IAppSettingsReader appSettingsReader) {
            if (null == appSettingsReader) throw new ArgumentNullException(nameof(appSettingsReader));

            AppSettingsReader = appSettingsReader;
        }


    }
}
