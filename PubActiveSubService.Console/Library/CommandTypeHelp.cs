using System.Text;

namespace PubActiveSubService {
    public static class CommandTypeHelp {
        public static string StandardOut() {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(Constants.CommandPrompt + "======================================================================================");
            stringBuilder.AppendLine(Constants.CommandPrompt + "     Help Instructions.....");
            stringBuilder.AppendLine(Constants.CommandPrompt + "01.");
            stringBuilder.AppendLine(Constants.CommandPrompt + "02.");
            stringBuilder.AppendLine(Constants.CommandPrompt + "03.");
            stringBuilder.AppendLine(Constants.CommandPrompt + "04.");
            stringBuilder.AppendLine(Constants.CommandPrompt + "05.");
            stringBuilder.AppendLine(Constants.CommandPrompt + "06.");
            stringBuilder.AppendLine(Constants.CommandPrompt + "07.");
            stringBuilder.AppendLine(Constants.CommandPrompt + "08.");
            stringBuilder.AppendLine(Constants.CommandPrompt + "09.");
            stringBuilder.AppendLine(Constants.CommandPrompt + "10.");
            stringBuilder.AppendLine(Constants.CommandPrompt + "11.");
            stringBuilder.AppendLine(Constants.CommandPrompt + "12.");
            stringBuilder.AppendLine(Constants.CommandPrompt + "======================================================================================");

            return stringBuilder.ToString();
        }
    }
}
