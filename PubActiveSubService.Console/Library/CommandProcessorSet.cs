using System;
public static class CommandProcessorSet {
    public static void StandardIn(string parameters = "") {
        parameters = parameters.Trim();
        if (parameters.Length <= 0) {
            Console.WriteLine(Constants.CommandPrompt + @"     [Set without parameters reads the dictionary of name-value-pairs.]");
            return;         
        }
        Console.WriteLine(Constants.CommandPrompt + @"     [Set with parameters saves a name-value-pair [Name:Value] to the dictionary.]");
    }

  
}