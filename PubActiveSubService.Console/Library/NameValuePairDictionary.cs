using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

public static class NameValuePairDictionary {
    private static List<NameValuePair> NameValuePairs = new List<NameValuePair>();

    static NameValuePairDictionary() { LoadFile(); }

    public static void Post(string name, string value) {
        name = name.Trim().ToLower();
        value = value.Trim();
        lock (NameValuePairs) {
            if (NameValuePairs.Exists(nameValuePair => nameValuePair.Name == name))
                NameValuePairs.Find(nameValuePair => nameValuePair.Name == name).Value = value;
            else
                NameValuePairs.Add(new NameValuePair() { Name = name, Value = value });

            SaveFile();
        }
    }
    public static IEnumerable<String> Select(string name) {
        name = name.Trim().ToLower();
        lock (NameValuePairs) {
            if (NameValuePairs.Exists(nameValuePair => nameValuePair.Name == name))
                yield return NameValuePairs.Find(nameValuePair => nameValuePair.Name == name).Value;
        }
    }

    public static IEnumerable<INameValuePair> Select() {
        lock (NameValuePairs) {
            foreach (var nameValuePair in NameValuePairs.ToArray())
                yield return nameValuePair.DeepClone();
        }
    }

    public static void LoadFile() {
        NameValuePairs = NameValuePairsFileInfo.Read();
    }

    public static void SaveFile() {
        NameValuePairsFileInfo.Write(NameValuePairs);
    }

    [Serializable]
    private class NameValuePair : INameValuePair {
        public string Name { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }

    private static class NameValuePairsFileInfo {
        private static readonly BadNasFileInfo BadNasFileInfo = null;
        private static readonly object SyncLock = new object();
        private static List<NameValuePair> NameValuePairs = null;

        static NameValuePairsFileInfo() {
            var directoryPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var filePath = Path.Combine(directoryPath, "Channels.json");
            var fileInfo = new FileInfo(filePath);
            BadNasFileInfo = new BadNasFileInfo(fileInfo);
        }

        public static void Write(List<NameValuePair> nameValuePairs) {
            try {
                lock (SyncLock) {
                    NameValuePairs = nameValuePairs.DeepClone();
                    BadNasFileInfo.WriteAllText(JsonConvert.SerializeObject(NameValuePairs));
                }
            } catch (Exception exception) {
                throw exception;
            }
        }

        public static List<NameValuePair> Read() {
            try {
                lock (SyncLock) {
                    if (null != NameValuePairs)
                        return NameValuePairs.DeepClone();

                    if (BadNasFileInfo.Exists()) {
                        var channelsJson = BadNasFileInfo.ReadAllText();
                        var channels = (List<NameValuePair>)JsonConvert.DeserializeObject<List<NameValuePair>>(channelsJson);
                        return channels;
                    }
                    return new List<NameValuePair>();
                }
            } catch (Exception exception) {
                throw exception;
            }
        }
    }
}