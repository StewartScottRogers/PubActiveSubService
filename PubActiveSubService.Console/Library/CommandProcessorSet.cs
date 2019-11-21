using System;
public static class CommandProcessorSet {
    public static void StandardIn(string parameters = "") {
        Console.WriteLine(Constants.CommandPrompt + "======================================================================================");
        Console.WriteLine(Constants.CommandPrompt + "     Set Instructions.....");
        Console.WriteLine(Constants.CommandPrompt + "======================================================================================");
    }
}