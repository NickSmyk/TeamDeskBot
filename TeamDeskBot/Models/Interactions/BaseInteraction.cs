namespace TeamDeskBot.Models.Interactions;

public abstract class BaseInteraction : IInteraction
{
    public Stage? CurrentStage { get; set; }
    public List<Stage> Stages { get; set; }

    protected BaseInteraction()
    {
        this.Stages = new List<Stage>();
        //TODO: QUESTION -> is it that bad
        // ReSharper disable once VirtualMemberCallInConstructor
        InitStages();
        this.CurrentStage = this.Stages.First();
    }

    public abstract string FinishInteraction();
    
    protected abstract void InitStages();

    public string GetDescription()
    {
        //TODO: WORK -> fix this
        return this.CurrentStage?.StageTask ?? "No stage is present";
    }
    
    public void ExecuteStage(string data)
    {
        if (this.CurrentStage is null)
        {
            return;
        }
        
        this.CurrentStage = this.CurrentStage.ExecuteStage(data);
    }

    protected void AddNewStage(Action<string> action, string fieldDescription)
    {
        Stage newStage = new(action, fieldDescription);
        Stage? previousStage = this.Stages.LastOrDefault();

        if (previousStage is null)
        {
            this.Stages.Add(newStage);
            return;
        }

        newStage.PreviousStage = previousStage;
        previousStage.NextStage = newStage;
        this.Stages.Add(newStage);
    }
}