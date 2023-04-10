using System.Text;
using Discord.Commands;
using TeamDeskBot.Attribute;
using TeamDeskBot.Exceptions;
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
        catch (BaseException ex)
        {
            Console.WriteLine(ex);
            await ReplyAsync(ex.DisplayMessage);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            await ReplyAsync(ErrorsHelper.ERROR_DURING_COMMAND_EXECUTION);
        }
    }

    [BotCommand(Commands.Commands)]
    public async Task GetCommands()
    {
        try
        {
            await _requestCommandsService.GetCommands(this.Context);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            await ReplyAsync(ErrorsHelper.ERROR_DURING_COMMAND_EXECUTION);
        }
    }

    [BotCommand(Commands.DeleteUser)]
    public async Task DeleteUser(string id)
    {
        try
        {
            await _requestCommandsService.DeleteUser(id);
        }
        catch (BaseException ex)
        {
            Console.WriteLine(ex);
            await ReplyAsync(ex.DisplayMessage);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            await ReplyAsync(ErrorsHelper.ERROR_DURING_COMMAND_EXECUTION);
        }
    }
}