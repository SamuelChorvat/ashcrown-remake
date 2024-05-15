using Ashcrown.Remake.Api.Dtos.Outbound;
using Ashcrown.Remake.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ashcrown.Remake.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
[ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
public class LoginController(IChampionDataService championDataService) : ControllerBase
{
    [HttpGet("version", Name = nameof(GetVersionInfo))]
    [ProducesResponseType(typeof(VersionInfo), StatusCodes.Status200OK)]
    public async Task<VersionInfo> GetVersionInfo()
    {
        return new VersionInfo
        {
            PlayableChampions = await championDataService.GetChampionsData()
        };
    }
}
