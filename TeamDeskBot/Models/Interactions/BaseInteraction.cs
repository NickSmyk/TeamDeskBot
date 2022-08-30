namespace TeamDeskBot.Models.Interactions;

public abstract class BaseInteraction : IInteraction
{
    public Stage CurrentStage { get; set; }
    public List<Stage> Stages { get; set; }
    public bool IsFinished { get; private set; }

    protected BaseInteraction()
    {
        this.Stages = new List<Stage>();
        //TODO: QUESTION -> is it that bad
        // ReSharper disable once VirtualMemberCallInConstructor
        InitStages();
        this.CurrentStage = this.Stages.First();
        this.IsFinished = false;
    }

    public abstract string FinishInteraction();
    
    protected abstract void InitStages();

    public string GetDescription()
    {
        //TODO: WORK -> fix this
        return this.CurrentStage.StageTask;
    }
    
    public void ExecuteStage(string data)
    {
        if (this.IsFinished)
        {
            return;
        }

        Stage? nextStage = this.CurrentStage.ExecuteStage(data);

        if (nextStage is null)
        {
            this.IsFinished = true;
            return;
        }
        
        this.CurrentStage = nextStage;
    }

    public void CancelStage()
    {
        Stage? previousStage = this.CurrentStage.PreviousStage;
        //TODO: WORK -> custom exception
        this.CurrentStage = previousStage ?? throw new Exception("An error occured during the execution");
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