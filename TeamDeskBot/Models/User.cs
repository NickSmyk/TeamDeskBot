namespace TeamDeskBot.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string DiscordId { get; set; }
    public string NicknameTg { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public TimeSpan WorkStart { get; set; }
    public TimeSpan WorkEnd { get; set; }
    public string TimeZone { get; set; }
    public string ScopeOfActivity { get; set; }
    public bool IsOnVacation { get; set; }
}