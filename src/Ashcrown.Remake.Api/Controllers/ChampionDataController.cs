using Ashcrown.Remake.Api.Dtos.Outbound;
using Ashcrown.Remake.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ashcrown.Remake.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
[ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
public class ChampionDataController(IChampionDataService championDataService) : ControllerBase
{
    [HttpGet(Name = nameof(GetChampionsData))]
    [ProducesResponseType(typeof(List<ChampionData>), StatusCodes.Status200OK)]
    public async Task<List<ChampionData>> GetChampionsData()
    {
        var championsData = await championDataService.GetChampionsData();
        return championsData;
    }
}