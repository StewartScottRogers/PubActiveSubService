public static class CommandProccessor {
    public static (CommandTypes CommandType, string Parameters) ProcessCommandLine(string commandLine) {
        commandLine = commandLine.Trim().ToLower();
        if (commandLine.StartsWith("exit")) return (CommandType: CommandTypes.Exit, Parameters: commandLine.TrimStart("exit".ToCharArray()).Trim());
        if (commandLine.StartsWith("help")) return (CommandType: CommandTypes.Help, Parameters: commandLine.TrimStart("help".ToCharArray()).Trim());
        if (commandLine.StartsWith("set")) return (CommandType: CommandTypes.Set, Parameters: commandLine.TrimStart("set".ToCharArray()).Trim());
        return (CommandType: CommandTypes.Unknown, Parameters: "");
    }
}