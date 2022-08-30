﻿using Discord.Commands;
using Discord.Interactions;
using TeamDeskBot.Models;
using TeamDeskBot.Models.Enums;
using TeamDeskBot.Models.Interactions;

namespace TeamDeskBot.Services;

public class InteractiveCommandsService
{
    private Dictionary<string, BaseInteraction> _interactions;

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