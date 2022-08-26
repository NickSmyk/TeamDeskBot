using TeamDeskBot.Models.Enums;
using Discord.Commands;

namespace TeamDeskBot.Attribute;

public class BotCommand : CommandAttribute
{
    public BotCommand(Commands command) : base(command.ToString()) { }
}