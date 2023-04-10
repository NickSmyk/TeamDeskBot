using System.Reflection;
using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using TeamDeskBot.Exceptions;
using TeamDeskBot.Helpers;
using InteractionType = TeamDeskBot.Models.Enums.InteractionType;
using IResult = Discord.Commands.IResult;

namespace TeamDeskBot.Services;


public class Bot
{
    private readonly string _token;
    private readonly DiscordSocketClient _client;
    private readonly CommandService _commands;
    private readonly IServiceProvider _services;
    private readonly InteractiveCommandsService _interactiveCommandsService;

    public Bot(string botToken)
    {
        _token = botToken;
        _client = new DiscordSocketClient();
        _commands = new CommandService();
        _interactiveCommandsService = new InteractiveCommandsService();
        ApiRequestsService apiRequestsService = new();
        RequestCommandsService requestCommandsService = new(apiRequestsService);
        
        _services = new ServiceCollection()
            .AddSingleton(_client)
            .AddSingleton(_commands)
            .AddSingleton(_interactiveCommandsService)
            .AddSingleton(apiRequestsService)
            .AddSingleton(requestCommandsService)
            .BuildServiceProvider();
    }
    
    public async Task RunAsync()
    {
        _client.Log += Log;
        await RegisterCommands();
        await _client.LoginAsync(TokenType.Bot, _token);
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
        //_client.ReactionAdded += ClientOnReactionAdded;
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
            await ProcessInteraction(context);
        }
        
        IResult? result = await _commands.ExecuteAsync(context, argPos, _services);
    }

    private async Task ProcessInteraction(SocketCommandContext context)
    {
        try
        {
            _interactiveCommandsService.ProcessInteraction(context);
        }
        catch (BaseException ex)
        {
            Console.WriteLine(ex);
            await _interactiveCommandsService.CancelInteraction(context, ex.DisplayMessage);
            return;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            const string cancelText = "An error occured during the execution, the interaction has been cancelled!";
            await _interactiveCommandsService.CancelInteraction(context, cancelText);
        }
    }
}