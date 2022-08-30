﻿using System.Reflection;
using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using TeamDeskBot.Helpers;
using InteractionType = TeamDeskBot.Models.Enums.InteractionType;
using IResult = Discord.Commands.IResult;

namespace TeamDeskBot.Services;


public class Bot
{
    private const string TOKEN = "MTAxMDIwNTE4NDY5MTAxNTczMQ.GKPtTC.LNXj-T62HhGOoQm8hceNrHkAiBfkgH6aD_a4dg";
    private DiscordSocketClient _client;
    private CommandService _commands;
    private IServiceProvider _services;
    private InteractiveCommandsService _interactiveCommandsService;
    private ApiRequestsService _apiRequestsService;
    private RequestCommandsService _requestCommandsService;

    public async Task RunAsync()
    {
        _client = new DiscordSocketClient();
        _commands = new CommandService();
        _interactiveCommandsService = new InteractiveCommandsService();
        _apiRequestsService = new ApiRequestsService();
        _requestCommandsService = new RequestCommandsService(_apiRequestsService);

        _services = new ServiceCollection()
            .AddSingleton(_client)
            .AddSingleton(_commands)
            .AddSingleton(_interactiveCommandsService)
            .AddSingleton(_apiRequestsService)
            .AddSingleton(_requestCommandsService)
            .BuildServiceProvider();

        _client.Log += Log;
        await RegisterCommands();
        await _client.LoginAsync(TokenType.Bot, TOKEN);
        await _client.StartAsync();

        await Task.Delay(-1);
    }

    private Task Log(LogMessage logMessage)
    {
        Console.WriteLine(logMessage);
        return Task.CompletedTask;
    }

    private async Task RegisterCommands()
    {
        _client.MessageReceived += HandleCommandAsync;
        await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
    }

    private async Task HandleCommandAsync(SocketMessage socketMessage)
    {
        if (socketMessage is not SocketUserMessage message)
        {
            Console.WriteLine("Bot received a null message");
            return;
        }
        
        SocketCommandContext context = new(_client, message);
        
        if (message.Author.IsBot)
        {
            return;
        }

        int argPos = 0;

        if (!message.HasStringPrefix(BotHelper.COMMAND_SYMBOL, ref argPos))
        {
            _interactiveCommandsService.ProcessInteraction(context);
        }
        
        IResult? result = await _commands.ExecuteAsync(context, argPos, _services);

        //TODO: QUESTION -> do I really need this
        if (result is null || !result.IsSuccess)
        {
            Console.WriteLine("Error");
        }
    }
}