using Ashcrown.Remake.Api.Models.Enums;

namespace Ashcrown.Remake.Api.Extensions;

public static class FindMatchTypeExtensions
{
    public static bool IsDraft(this FindMatchType matchType)
    {
        return matchType is FindMatchType.DraftPrivate or FindMatchType.DraftPublic;
    }
}