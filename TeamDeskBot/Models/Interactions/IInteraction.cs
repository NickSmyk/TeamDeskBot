﻿namespace TeamDeskBot.Models.Interactions;

public interface IInteraction
{
    Stage CurrentStage { get; set; }
    List<Stage> Stages { get; set; }
    void ExecuteStage(string data);
}