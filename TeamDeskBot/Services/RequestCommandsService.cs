using System.Text;
using Discord.Commands;
using TeamDeskBot.Extensions;
using TeamDeskBot.Helpers;
using TeamDeskBot.Models;
using TeamDeskBot.Models.Enums;

namespace TeamDeskBot.Services;

public class RequestCommandsService
{
    private readonly ApiRequestsService _apiRequestsService;

    public RequestCommandsService(ApiRequestsService apiRequestsService)
    {
        _apiRequestsService = apiRequestsService;
    }
    public async Task GetUsers(SocketCommandContext context)
    {
        //TODO: QUESTION -> can I make it better
        IEnumerable<UserViewModel> users = await _apiRequestsService.GetUsers();
        int counter = 0;
        
        foreach (UserViewModel user in users)
        {
            counter++;
            StringBuilder message = new();
            //TODO: WORK -> move this into a different method??
            message.AppendLine($"**{user.Id}** - Id of the user");
            message.AppendLine($"**{user.Name}** - Name of the person");
            message.AppendLine($"**{user.NicknameDis}** - Nickname in discord");
            message.AppendLine($"**{user.NicknameTG}** - Nickname in telegram");
            
            if (counter != users.Count())
            {
                message.AppendLine("** **");
            }
            
            await context.Channel.SendMessageAsync(message.ToString());
            await Task.Delay(TimeSpan.FromSeconds(BotHelper.SPAM_TO_DISCORD_API_DELAY));
        }
    }
    
    public async Task GetCommands(SocketCommandContext context)
    {
        StringBuilder builder = new();
        builder.AppendLine("Here is the list of all the available commands. You can use commands regardless of the case.");
        builder.AppendLine();
        Commands[] commands = (Commands[])Enum.GetValues(typeof(Commands));

        foreach (Commands command in commands)
        {
            builder.AppendLine($"{BotHelper.COMMAND_SYMBOL}{command} - {command.GetDescription()}");
        }
        
        await context.Channel.SendMessageAsync(builder.ToString());
    }

    public async Task DeleteUser(string id)
    {
        if (!int.TryParse(id, out int userId))
        {
            //TODO: WORK -> custom excpetion
            throw new Exception("An error occured during the execution");
        }

        await _apiRequestsService.DeleteUser(userId);
    }
}