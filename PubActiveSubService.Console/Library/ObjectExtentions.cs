using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class ObjectExtentions {
    public static T DeepClone<T>(this T a) {
        using (var memoryStream = new MemoryStream()) {
            var formatter = new BinaryFormatter();
            formatter.Serialize(memoryStream, a);
            memoryStream.Position = 0;
            return (T)formatter.Deserialize(memoryStream);
        }
    }
}

