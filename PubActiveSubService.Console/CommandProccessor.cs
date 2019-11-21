namespace PubActiveSubService {
    public static class CommandProccessor {
        public static (ResponseTypes ResponseType, string Parameters) ProcessCommandLine(string commandLine) {
            commandLine = commandLine.Trim().ToLower();
            if (commandLine.StartsWith("exit")) return (ResponseType: ResponseTypes.Exit, Parameters: "");
            if (commandLine.StartsWith("help")) return (ResponseType: ResponseTypes.Help, Parameters: "");
            if (commandLine.Equals("set")) return (ResponseType: ResponseTypes.SetRead, Parameters: "");
            if (commandLine.StartsWith("help")) return (ResponseType: ResponseTypes.SetWrite, Parameters: "");






            return (ResponseType: ResponseTypes.Unknown, Parameters: "");
        }
    }
}
