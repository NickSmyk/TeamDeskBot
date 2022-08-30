namespace TeamDeskBot.Models.Interactions;

public sealed class AddUserInteraction : BaseInteraction
{
    private User Result { get; set; }

    public AddUserInteraction()
    {
        this.Result = new User();
        this.Stages = new List<Stage>();
        AddNewStage(text => this.Result.Name = text, "Name");
        AddNewStage(text => this.Result.NicknameDis = text, "Discord nickname");
        AddNewStage(text => this.Result.NicknameTG = text, "Telegram nickname");
        this.CurrentStage = this.Stages.First();
    }

    protected override string OnInteractionFinished()
    {
        User user = this.Result;
        return "User added";
    }
}