using System;

public static class Program {
    static void Main() {
        Console.WriteLine(@"PubActiveSubService Client...");
        while (true) {
            Console.Write(Constants.CommandPrompt);
            var responseType = CommandProccessor.ProcessCommandLine(Console.ReadLine());
            if (CommandTypes.Exit == responseType.CommandType) return;
            if (CommandTypes.Help == responseType.CommandType) CommandProcessorHelp.StandardIn(responseType.Parameters);
            if (CommandTypes.Set == responseType.CommandType) CommandProcessorSet.StandardIn(responseType.Parameters);



        }
    }
}