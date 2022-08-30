using System.Text;
using Discord.Commands;
using Discord.Interactions;
using TeamDeskBot.Attribute;
using TeamDeskBot.Extensions;
using TeamDeskBot.Helpers;
using TeamDeskBot.Models;
using TeamDeskBot.Models.Enums;

namespace TeamDeskBot.Services;

public class CommandsService : ModuleBase<SocketCommandContext>
{
    private readonly ApiRequestsService _apiRequestsService;
    private readonly InteractionsHandler _interactionsHandler;

    public CommandsService(InteractionsHandler interactionsHandler)
    {
        _interactionsHandler = interactionsHandler;
        _apiRequestsService = new ApiRequestsService();
    }
    
    [Command("Ping")]
    public async Task Ping()
    {
        await ReplyAsync("Pong");
    }

    [BotCommand(Commands.GetUsers)]
    public async Task GetUsers()
    {
        IEnumerable<User> users = await _apiRequestsService.GetUsers();
        foreach (User user in users)
        {
            string message = $"{user.Name}\n{user.NicknameDis}\n{user.NicknameTG}";
            await ReplyAsync(message);
        }
    }

    [BotCommand(Commands.Commands)]
    public async Task GetCommands()
    {
        StringBuilder builder = new();
        builder.AppendLine("Here is the list of all the available commands. You can use commands regardless of the case.");
        builder.AppendLine();
        Commands[] commands = (Commands[])Enum.GetValues(typeof(Commands));

        foreach (Commands command in commands)
        {
            builder.AppendLine($"{BotHelper.COMMAND_SYMBOL}{command} - {command.GetDescription()}");
        }
        
        await ReplyAsync(builder.ToString());
    }
}