namespace TeamDeskBot.Models.Interactions;

public class Stage
{
    public string StageTask { get; }
    public Stage? NextStage { get; set; }
    public Stage? PreviousStage { get; set; }

    private readonly Action<string> _stageAction;

    //TODO: QUESTION -> maybe people should be able to fully specify the message
    public Stage(Action<string> stageAction, string inputDescription)
    {
        this.StageTask = $"Please, enter {inputDescription}";
        _stageAction = stageAction;
    }
    
    public Stage? ExecuteStage(string message)
    {
        _stageAction.Invoke(message);
        return this.NextStage!;
    }
}