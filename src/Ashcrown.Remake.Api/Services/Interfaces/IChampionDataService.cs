using Ashcrown.Remake.Api.Dtos.Outbound;

namespace Ashcrown.Remake.Api.Services.Interfaces;

public interface IChampionDataService
{
    Task<List<ChampionData>> GetChampionsData();
}