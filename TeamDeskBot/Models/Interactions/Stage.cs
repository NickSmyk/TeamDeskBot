namespace TeamDeskBot.Models.Interactions;

public class Stage
{
    public string StageTask { get; }
    public Stage? NextStage { get; set; }
    public Stage? PreviousStage { get; set; }

    private readonly Action<string> _stageAction;

    public Stage(Action<string> stageAction, string filedDescription)
    {
        this.StageTask = $"Please, enter {filedDescription}";
        _stageAction = stageAction;
    }
    
    public Stage? ExecuteStage(string message)
    {
        _stageAction.Invoke(message);
        return this.NextStage!;
    }
}