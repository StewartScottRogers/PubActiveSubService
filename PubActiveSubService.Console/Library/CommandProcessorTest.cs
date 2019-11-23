using Library;
using System;
using System.Linq;

public static class CommandProcessorTest {
    public static void StandardIn(string parameters = "") {
        try {

            var channels = TestBuilder.GetTestChannelsAndSubscribers().ToArray();

            parameters = parameters.Trim();
            if (parameters.Length <= 0) {

                // List all Tests

                return;
            }

            // Run the selected Test

        } catch (Exception exception) {
            Console.Error.WriteLine(Constants.CommandPrompt + exception.Message);
        }
    }
}
