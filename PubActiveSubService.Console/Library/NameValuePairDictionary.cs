using System;
using System.Collections.Generic;

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

    }

    public static void SaveFile() {

    }

    [Serializable]
    private class NameValuePair : INameValuePair {
        public string Name { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }
}