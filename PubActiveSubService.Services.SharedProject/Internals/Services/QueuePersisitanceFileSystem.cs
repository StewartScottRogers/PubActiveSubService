using PubActiveSubService.Internals.Interfaces;
using System;

namespace PubActiveSubService.Internals.Services {
    public class QueuePersisitanceFileSystem : IQueuePersisitance {
        private readonly IAppSettingsReader AppSettingsReader;

        public QueuePersisitanceFileSystem(IAppSettingsReader appSettingsReader) {
            if (null == appSettingsReader) throw new ArgumentNullException(nameof(appSettingsReader));

            AppSettingsReader = appSettingsReader;
        }


    }
}
