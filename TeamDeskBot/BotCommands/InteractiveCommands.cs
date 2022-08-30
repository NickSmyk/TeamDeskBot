﻿using Discord.Commands;
using TeamDeskBot.Attribute;
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
    
}