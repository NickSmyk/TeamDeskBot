namespace TeamDeskBot.Helpers;

public static class BotHelper
{
    public const string COMMAND_SYMBOL = "!";
    
    /// <summary>
    /// <para>According to the documentation bots are allowed to make up to 50 requests per second but I set limit to 20 just in case</para>
    /// <see href="https://discord.com/developers/docs/topics/rate-limits" langword="LINK IN THE HREF"/> => Paragraph: Global Rate Limit
    /// </summary>
    public const float SPAM_TO_DISCORD_API_DELAY = (float) 1/20;
}