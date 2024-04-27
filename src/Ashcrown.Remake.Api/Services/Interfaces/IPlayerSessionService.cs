using Ashcrown.Remake.Api.Models;

namespace Ashcrown.Remake.Api.Services.Interfaces;

public interface IPlayerSessionService
{
    Task<bool> CreateSession(string playerName);
    Task<bool> RemoveSession(string playerName);
    Task<PlayerSession?> GetSession(string playerName);
    Task UpdateSession(string playerName, Action<PlayerSession> updateAction);
    Task<IList<string>> GetCurrentInUsePlayerNames();
    Task<int> RemoveStaleSessions(int staleSessionLimitInMinutes);
}