using Microsoft.Extensions.Configuration;
using TeamDeskBot.Services;

IConfigurationRoot config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
string botToken = config["BotToken"];
Bot bot = new(botToken);
await bot.RunAsync();