namespace TeamDeskBot.Models.Interactions;

public class Stage
{
    public string StageTask { get; }
    public Stage? NextStage { get; set; }
    public Stage? PreviousStage { get; set; }

    public Action<string> StageAction;

    public Stage(Action<string> stageAction, string filedDescription)
    {
        this.StageTask = $"Please, enter {filedDescription}";
        StageAction = stageAction;
    }
    
    public Stage? ExecuteStage(string message)
    {
        this.StageAction.Invoke(message);
        return this.NextStage!;
    }
}