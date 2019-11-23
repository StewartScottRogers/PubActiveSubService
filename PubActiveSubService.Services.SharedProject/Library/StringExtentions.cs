using System.Linq;

public static class StringExtentions {
    public static string ToEnforcedChannelNamingConventions(this string channelName) { 
        return channelName.Trim().ToLower().ToValidFilePathing();
    }

    public static string ToEnforcedSubscriberNamingConventions(this string subscriberName) {
        return subscriberName.Trim().ToLower().ToValidFilePathing();
    }

    public static string ToEnforcedUrlNamingStandards(this string url) {
        url = url.Trim().ToLower();
        if (url == "string")
            url = string.Empty;

        return url;
    }

    public static string ToEnforceChannelSearchNamingConventions(this string channelSearch) {
        channelSearch = channelSearch.Trim().ToLower();
        if (channelSearch == "string")
            channelSearch = "*";

        var hasWildCard = channelSearch.EndsWith("*");
        channelSearch = channelSearch.ToValidFilePathing();
        if (hasWildCard)
            channelSearch += "*";

        return channelSearch;
    }

    private static string ToValidFilePathing(this string text) =>
         new string(text.Where(c => char.IsLetterOrDigit(c) || c == '\\' || c == '/').ToArray()).Replace("/", @"\").Trim();

}
