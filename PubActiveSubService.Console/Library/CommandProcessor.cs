public static class CommandProcessor {
    public static (CommandTypes CommandType, string Parameters) ProcessCommandLine(string commandLine) {
        commandLine = commandLine.Trim().ToLower();
        if (commandLine.StartsWith("exit")) return (CommandType: CommandTypes.Exit, Parameters: commandLine.TrimStart("exit".ToCharArray()).Trim());
        if (commandLine.StartsWith("help")) return (CommandType: CommandTypes.Help, Parameters: commandLine.TrimStart("help".ToCharArray()).Trim());
        if (commandLine.StartsWith("set")) return (CommandType: CommandTypes.Set, Parameters: commandLine.TrimStart("set".ToCharArray()).Trim());
        if (commandLine.StartsWith("test")) return (CommandType: CommandTypes.Test, Parameters: commandLine.TrimStart("test".ToCharArray()).Trim());
        return (CommandType: CommandTypes.Unknown, Parameters: "");
    }
}