﻿namespace Ashcrown.Remake.Core.Battle.Models.Dtos.Outbound;

public class TargetsUpdate
{
    public required int ChampionNo { get; set; }
    public required int AbilityNo { get; set; }
    public required int[] Targets { get; set; }
}