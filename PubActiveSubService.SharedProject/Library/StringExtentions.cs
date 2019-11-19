using System.Linq;

public static class StringExtentions {
    public static string ToEnforcedChannelNamingConventions(this string channelName) {
        return channelName.Trim().ToLower().ToAlphaNumeric();
    }
    public static string ToEnforceChannelSearchNamingConventions(this string channelName) {
        channelName = channelName.Trim().ToLower();
        if (channelName == "string")
            channelName = "*";

        var hasWildCard = channelName.EndsWith("*");
        channelName = channelName.ToAlphaNumeric();
        if (hasWildCard)
            channelName += "*";

        return channelName;
    }

    private static string ToAlphaNumeric(this string text) =>
         new string(text.Where(c => char.IsLetterOrDigit(c) || c == '\\' || c == '/').ToArray()).Replace("/", @"\").Trim();

}
