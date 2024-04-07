﻿namespace Ashcrown.Remake.Core.Battle.Models.Dtos;

public class PlayerUpdate
{
    public int? TurnTime { get; set; }
    public int EnergyExchangeRatio { get; set; }
    public string[] MyChampions { get; set; } = new string[3];
    public string[] OpponentChampions { get; set; } = new string[3];
    public required int[] Energy { get; set; }
    public MyChampionUpdate[] MyChampionUpdates { get; set; } = new MyChampionUpdate[3];
    public OpponentChampionUpdate[] OpponentChampionUpdates { get; set; } = new OpponentChampionUpdate[3];
}