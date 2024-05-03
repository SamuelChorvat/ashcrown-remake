using Ashcrown.Remake.Api.Models;

namespace Ashcrown.Remake.Api.Services.Interfaces;

public interface IPlayerSessionService
{
    Task<PlayerSession?> CreateSession(string playerName);
    Task<bool> RemoveSession(string playerName, string secret);
    Task<PlayerSession?> GetSession(string playerName);
    Task UpdateSession(string playerName, string secret, Action<PlayerSession> updateAction);
    Task<IList<string>> GetCurrentInUsePlayerNames();
    Task<int> RemoveStaleSessions(int staleSessionLimitInMinutes);
    Task ValidateProvidedSecret(string playerName, string secret);
}