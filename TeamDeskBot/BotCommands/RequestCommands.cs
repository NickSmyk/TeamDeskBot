using System.Text;
using Discord.Commands;
using TeamDeskBot.Attribute;
using TeamDeskBot.Extensions;
using TeamDeskBot.Helpers;
using TeamDeskBot.Models;
using TeamDeskBot.Models.Enums;
using TeamDeskBot.Services;

namespace TeamDeskBot.BotCommands;

public class RequestCommands : ModuleBase<SocketCommandContext>
{
    private RequestCommandsService _requestCommandsService;

    public RequestCommands(RequestCommandsService requestCommandsService)
    {
        _requestCommandsService = requestCommandsService;
    }

    [BotCommand(Commands.GetUsers)]
    public async Task GetUsers()
    {
        try
        {
            await _requestCommandsService.GetUsers(this.Context);

        }
        catch
        {
            
        }
        
    }

    [BotCommand(Commands.Commands)]
    public async Task GetCommands()
    {
        try
        {
            await _requestCommandsService.GetCommands(this.Context);
        }
        catch
        {
            
        }
    }

    [BotCommand(Commands.DeleteUser)]
    public async Task DeleteUser(string id)
    {
        try
        {
            await _requestCommandsService.DeleteUser(id);
        }
        catch
        {
            
        }
    }
}