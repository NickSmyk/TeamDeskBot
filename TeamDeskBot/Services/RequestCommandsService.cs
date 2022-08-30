﻿using System.Text;
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
            message.AppendLine(user.Id);
            message.AppendLine(user.Name);
            message.AppendLine(user.NicknameDis);
            message.AppendLine(user.NicknameTG);
            
            await context.Channel.SendMessageAsync(message.ToString());
            
            if (counter != users.Count())
            {
                await context.Channel.SendMessageAsync("** **");
            }
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
}