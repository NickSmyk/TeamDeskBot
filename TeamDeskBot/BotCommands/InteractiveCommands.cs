using Discord.Commands;
using TeamDeskBot.Attribute;
using TeamDeskBot.Models;
using TeamDeskBot.Models.Enums;
using TeamDeskBot.Models.Interactions;
using TeamDeskBot.Services;

namespace TeamDeskBot.BotCommands;

public class InteractiveCommands : ModuleBase<SocketCommandContext>
{
    private readonly InteractiveCommandsService _interactiveCommandsService;
    private readonly ApiRequestsService _apiRequestsService;

    public InteractiveCommands(InteractiveCommandsService interactiveCommandsService, ApiRequestsService apiRequestsService)
    {
        _interactiveCommandsService = interactiveCommandsService;
        _apiRequestsService = apiRequestsService;
    }

    [BotCommand(Commands.AddUser)]
    public async Task AddUser()
    {
        BaseInteraction newInteraction = new AddUserInteraction(_apiRequestsService);
        await _interactiveCommandsService.StartInteraction(this.Context, newInteraction);
    }

    [BotCommand(Commands.EditUser)]
    public async Task EditUser(int userId)
    {
        User user = await _apiRequestsService.GetUser(userId);
        BaseInteraction newInteraction = new EditUserInteraction(_apiRequestsService, user);
        await _interactiveCommandsService.StartInteraction(this.Context, newInteraction);
    }

    [BotCommand(Commands.Back)]
    public async Task CancelStage()
    {
        await _interactiveCommandsService.CancelStage(this.Context);
    }

    [BotCommand(Commands.Cancel)]
    public async Task CancelInteraction()
    {
        await _interactiveCommandsService.CancelInteraction(this.Context);
    }
}