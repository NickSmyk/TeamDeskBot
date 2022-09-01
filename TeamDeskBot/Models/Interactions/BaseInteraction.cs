using TeamDeskBot.Exceptions;

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
        return this.CurrentStage.StageTask;
    }

    //TODO: WORK -> add exception
    public bool ExecuteStage(string data)
    {
        if (this.IsFinished)
        {
            return true;
        }

        try
        {
            Stage? nextStage = this.CurrentStage.ExecuteStage(data);

            if (nextStage is null)
            {
                this.IsFinished = true;
                return true;
            }

            this.CurrentStage = nextStage;
            return true;
        }
        catch (BaseException ex)
        {
            Console.WriteLine(ex);
            return false;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public void CancelStage()
    {
        Stage? previousStage = this.CurrentStage.PreviousStage;
        //TODO: WORK -> custom exception
        this.CurrentStage = previousStage ?? throw new Exception("An error occured during the execution");
    }

    protected void AddNewStage(Action<string> action, string inputDescription)
    {
        Stage newStage = new(action, inputDescription);
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