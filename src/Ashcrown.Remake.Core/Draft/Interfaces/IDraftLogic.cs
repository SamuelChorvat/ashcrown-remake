using Ashcrown.Remake.Core.Draft.Dtos.Outbound;
using Ashcrown.Remake.Core.Draft.Enums;

namespace Ashcrown.Remake.Core.Draft.Interfaces;

public interface IDraftLogic
{
    string?[,] PickedChampions { get; }
    DraftState DraftState { get; }
    DraftStatus[] DraftStatuses { get; set; }
    string[] Players { get; init; }
    DateTime TurnStartTime { get; }
    DateTime EndDraftTime { get; set; }
    string WhoseTurn { get; set; }

    void SelectBan(string championName);
    void ConfirmBan();
    void SelectPick(string championName);
    void ConfirmPick();
    void Surrender(string playerName);
    int GetPlayerIndex(string playerName);
    DraftStatusUpdate GetDraftUpdate(string playerName);
}