public static class StringExtentions {
    public static string ToEnforceChannelNamingConventions(this string channelName) {
        return channelName.Trim().ToLower();
    }
}
