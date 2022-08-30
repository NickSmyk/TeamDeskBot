using Discord.Commands;
using Discord.WebSocket;
using TeamDeskBot.Attribute;
using TeamDeskBot.Models.Enums;

namespace TeamDeskBot.Services;

public class DialogueService : ModuleBase<SocketCommandContext>
{
    private readonly InteractionsHandler _interactionsHandler;

    public DialogueService(InteractionsHandler interactionsHandler)
    {
        _interactionsHandler = interactionsHandler;
    }

    [BotCommand(Commands.AddUser)]
    public async Task Ping()
    {
        await _interactionsHandler.StartInteraction(this.Context, InteractionType.AddUser);
    }
    
}