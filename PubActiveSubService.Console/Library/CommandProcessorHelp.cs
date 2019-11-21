using System;
public static class CommandProcessorHelp {
    public static void StandardIn(string parameters = "") {
        Console.WriteLine(Constants.CommandPrompt + @"======================================================================================");
        Console.WriteLine(Constants.CommandPrompt + @"     Help Instructions.....");
        Console.WriteLine(Constants.CommandPrompt + @"01: Set [Name:'Value']");
        Console.WriteLine(Constants.CommandPrompt + "======================================================================================");
    }
    private static void HelpTopics(string parameters) {
        if (parameters.StartsWith("set")) { HelpTopicSet(parameters.TrimStart("set".ToCharArray()).Trim()); return; };
    }

    private static void HelpTopicSet(string parameters) {
        parameters = parameters.Trim();
        if (parameters.Length <= 0) {
            Console.WriteLine(Constants.CommandPrompt + @"======================================================================================");
            Console.WriteLine(Constants.CommandPrompt + @"     Set without parameters reads the dictionary of name-value-pairs.");
            Console.WriteLine(Constants.CommandPrompt + @"======================================================================================");
            return;
        }

        Console.WriteLine(Constants.CommandPrompt + @"======================================================================================");
        Console.WriteLine(Constants.CommandPrompt + @"     Set with parameters saves a name-value-pair [Name:Value] to the dictionary.");
        Console.WriteLine(Constants.CommandPrompt + @"======================================================================================");
    }
}