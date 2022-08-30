using Discord.Commands;
using Discord.Interactions;
using TeamDeskBot.Models;
using TeamDeskBot.Models.Enums;
using TeamDeskBot.Models.Interactions;

namespace TeamDeskBot.Services;

public class InteractionsHandler
{
    private Dictionary<string, BaseInteraction> _interactions;

    public InteractionsHandler()
    {
        _interactions = new Dictionary<string, BaseInteraction>();
    }

    public async Task StartInteraction(SocketCommandContext context, InteractionType type)
    {
        if (_interactions.ContainsKey(context.User.Username))
        {
            string inWorkMessage = $"User currently has an interaction {type.ToString()}";
            await context.Channel.SendMessageAsync(inWorkMessage).ConfigureAwait(false);
            return;
        }

        BaseInteraction newInteraction = new AddUserInteraction();
        _interactions.TryAdd(context.User.Username, newInteraction);
        await context.Channel.SendMessageAsync(newInteraction.GetDescription()).ConfigureAwait(false);
    }

    public async void ProcessInteraction(SocketCommandContext context)
    {
        string key = context.User.Username;

        if (!_interactions.TryGetValue(key, out BaseInteraction? interaction))
        {
            //TODO: WORK -> resolve this
            throw new Exception("An error occured during the execution");
        }

        if (interaction is null)
        {
            //TODO: WORK -> resolve this\\ Send message???
            throw new Exception("An error occured during the execution");
        }
        
        string data = context.Message.Content;
        interaction.ExecuteStage(data);
        
        if (interaction.CurrentStage is null)
        {
            string message = interaction.FinishInteraction();
            await context.Channel.SendMessageAsync(message).ConfigureAwait(false);
            _interactions.Remove(key);
            return;
        }
        
        await context.Channel.SendMessageAsync(interaction.GetDescription()).ConfigureAwait(false);
    }
}