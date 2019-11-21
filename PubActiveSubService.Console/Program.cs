using System;

namespace PubActiveSubService {
    public static class Program {
        static void Main() {
            while (true) {
                Console.Write(Constants.CommandPrompt);
                var responseType = CommandProccessor.ProcessCommandLine(Console.ReadLine());
                if (ResponseTypes.Exit == responseType.ResponseType) return;
                if (ResponseTypes.Help == responseType.ResponseType) Console.Write(CommandTypeHelp.StandardOut());


            }
        }
    }
}
