using TeamDeskBot.Exceptions;
using TeamDeskBot.Services;

namespace TeamDeskBot.Models.Interactions;

public sealed class AddUserInteraction : BaseInteraction
{
    private User Result { get; }
    private readonly ApiRequestsService _apiRequestsService;

    public AddUserInteraction(ApiRequestsService apiRequestsService)
    {
        _apiRequestsService = apiRequestsService;
        this.Result = new User();
    }

    public override string FinishInteraction()
    {
        try
        {
            _apiRequestsService.AddUser(this.Result).GetAwaiter().GetResult();
            return "User added";
        }
        catch (BaseException ex)
        {
            Console.WriteLine(ex);
            return ex.DisplayMessage;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return "An error occured while adding user";
        }
    }

    protected override void InitStages()
    {
        AddNewStage(text => this.Result.Name = text, "Name");
        AddNewStage(text => this.Result.DiscordId = text, "Discord id");
        AddNewStage(text => this.Result.NicknameTg = text, "Telegram nickname");
        AddNewStage(text => this.Result.Country = text, "Country");
        AddNewStage(text => this.Result.City = text, "City");
        AddNewStage(text => this.Result.WorkStart = TimeSpan.Parse(text), "Time when starts work");
        AddNewStage(text => this.Result.WorkEnd = TimeSpan.Parse(text), "Time when ends work");
        AddNewStage(text => this.Result.TimeZone = text, "Country");
        AddNewStage(text => this.Result.ScopeOfActivity = text, "Country");
        AddNewStage(text => this.Result.IsOnVacation = Boolean.Parse(text), "Country");
    }
}