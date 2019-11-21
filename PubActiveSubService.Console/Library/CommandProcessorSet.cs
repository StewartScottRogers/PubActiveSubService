using System;
using System.Linq;

public static class CommandProcessorSet {
    public static void StandardIn(string parameters = "") {
        try {
            parameters = parameters.Trim();
            if (parameters.Length <= 0) {
                var nameValuePairs = NameValuePairDictionary.Select().ToArray();
                foreach (var nameValuePair in nameValuePairs)
                    Console.WriteLine(Constants.CommandPrompt + $"{nameValuePair.Name}: {nameValuePair.Value}");
                return;
            }

            var parsedParameters = ParseParameters(parameters);
            NameValuePairDictionary.Post(parsedParameters.Name, parsedParameters.Value);
        }catch(Exception exception) {
            Console.Error.WriteLine(Constants.CommandPrompt + exception.Message);
        }
    }

    private static (string Name, string Value) ParseParameters(string parameters) {
        var name = "";
        var value = "";

        parameters = parameters.Trim();

        var deliniatorFound = false;
        foreach (var c in parameters)
            if (c == ':')
                deliniatorFound = true;
            else {
                if (!deliniatorFound)
                    name += c;
                else
                    value += c;
            }

        if (!deliniatorFound)
            throw new ApplicationException($"No Deliniator ':' found in '{parameters}'.");

        return (Name: name.Trim().ToLower(), Value: value.Trim());
    }
}
