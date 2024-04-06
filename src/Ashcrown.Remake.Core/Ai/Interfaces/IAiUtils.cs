using Ashcrown.Remake.Core.Ai.Models;

namespace Ashcrown.Remake.Core.Ai.Interfaces;

public interface IAiUtils
{
    static abstract AiMaximizedAbility GetHigherPointsAbility(AiMaximizedAbility? ability1, AiMaximizedAbility ability2);
}