using System;
public static class CommandProcessorHelp {
    public static void StandardIn(string parameters = "") {
        Console.WriteLine(Constants.CommandPrompt + @"======================================================================================");
        Console.WriteLine(Constants.CommandPrompt + @"     Help Instructions.....");
        Console.WriteLine(Constants.CommandPrompt + @"Set [Name:'Value']");
        Console.WriteLine(Constants.CommandPrompt + "======================================================================================");
    }
}