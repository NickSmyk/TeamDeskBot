using Discord.Commands;
using TeamDeskBot.Exceptions;
using TeamDeskBot.Models.Interactions;

namespace TeamDeskBot.Services;

//TODO: WORK -> move into a different class
public class InteractiveCommandsService
{
    private Dictionary<string, BaseInteraction> _interactions;
    private const string STAGE_CANCELLED_MESSAGE = "Stage cancelled, going to the previous stage!";
    private const string INTERACTION_CANCELLED_MESSAGE = "Interaction cancelled!";

    public InteractiveCommandsService()
    {
        _interactions = new Dictionary<string, BaseInteraction>();
    }

    public async Task StartInteraction(SocketCommandContext context, BaseInteraction newInteraction)
    {
        string user = context.User.Username;
        
        if (_interactions.TryGetValue(user, out BaseInteraction? interaction))
        {
            string inWorkMessage = $"User currently has an interaction. {interaction.GetDescription()}";
            await context.Channel.SendMessageAsync(inWorkMessage).ConfigureAwait(false);
            return;
        }

        _interactions.TryAdd(context.User.Username, newInteraction);
        await context.Channel.SendMessageAsync(newInteraction.GetDescription()).ConfigureAwait(false);
    }

    public async void ProcessInteraction(SocketCommandContext context)
    {
        string key = context.User.Username;

        if (!_interactions.TryGetValue(key, out BaseInteraction? interaction))
        {
            return;
        }
        
        string data = context.Message.Content;
        interaction.ExecuteStage(data);
        
        if (interaction.IsFinished)
        {
            string message = interaction.FinishInteraction();
            await context.Channel.SendMessageAsync(message).ConfigureAwait(false);
            _interactions.Remove(key);
            return;
        }
        
        await context.Channel.SendMessageAsync(interaction.GetDescription()).ConfigureAwait(false);
    }

    public async Task CancelStage(SocketCommandContext context, string cancelMessage = STAGE_CANCELLED_MESSAGE)
    {
        string user = context.User.Username;

        if (!_interactions.TryGetValue(user, out BaseInteraction? interaction))
        {
            throw new NoInteractionException(user);
        }

        try
        {
            interaction.CancelStage();
        }
        catch (BaseException ex)
        {
            Console.WriteLine(ex);
            await context.Channel.SendMessageAsync(ex.DisplayMessage).ConfigureAwait(false);
            return;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            const string message = "Wasn't able to go back in the current interaction";
            await context.Channel.SendMessageAsync(message).ConfigureAwait(false);
            return;
        }
        
        await context.Channel.SendMessageAsync(cancelMessage).ConfigureAwait(false);
        await context.Channel.SendMessageAsync(interaction.GetDescription()).ConfigureAwait(false);
    }

    public async Task CancelInteraction(SocketCommandContext context, string message = INTERACTION_CANCELLED_MESSAGE)
    {
        string key = context.User.Username;
        _interactions.Remove(key);
        await context.Channel.SendMessageAsync(message).ConfigureAwait(false);
    }
}