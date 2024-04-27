using Ashcrown.Remake.Api.Dtos.Outbound;
using Microsoft.AspNetCore.Mvc;

namespace Ashcrown.Remake.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class VersionController : ControllerBase
{
    [HttpGet(Name = nameof(GetVersionInfo))]
    [ProducesResponseType(typeof(VersionInfo), StatusCodes.Status200OK)]
    public Task<VersionInfo> GetVersionInfo()
    {
        return Task.FromResult(new VersionInfo());
    }
}
