using System.Collections.Concurrent;
using Ashcrown.Remake.Api.Models;
using Ashcrown.Remake.Api.Services.Interfaces;

namespace Ashcrown.Remake.Api.Services;

public class PlayerSessionService : IPlayerSessionService
{
    private readonly ConcurrentDictionary<string, PlayerSession> _sessions = [];

    public Task<PlayerSession?> CreateSession(string playerName)
    {
        _sessions.TryAdd(playerName, new PlayerSession
        {
            PlayerName = playerName
        });
        _sessions.TryGetValue(playerName, out var session);
        return Task.FromResult(session);
    }

    public Task<bool> RemoveSession(string playerName, string secret)
    {
        ValidateProvidedSecret(playerName, secret);
        return Task.FromResult(_sessions.TryRemove(playerName, out _));
    }

    public Task<PlayerSession?> GetSession(string playerName)
    {
        _sessions.TryGetValue(playerName, out var playerSession);
        return Task.FromResult(playerSession);
    }

    public Task UpdateSession(string playerName, string secret, Action<PlayerSession> updateAction)
    {
        ValidateProvidedSecret(playerName, secret);
        _sessions.AddOrUpdate(playerName,
            key => throw new KeyNotFoundException($"No session found for player {key}"), 
            (_, existingSession) =>
            {
                updateAction(existingSession);
                return existingSession; 
            });

        return Task.CompletedTask;
    }

    public Task<IList<string>> GetCurrentInUsePlayerNames()
    {
        return Task.FromResult<IList<string>>(_sessions.Keys.ToList());
    }

    public Task<int> RemoveStaleSessions(int staleSessionLimitInMinutes)
    {
        var cutoffTime = DateTime.UtcNow.AddMinutes(-staleSessionLimitInMinutes);
        var keysToRemove = _sessions.Where(pair => pair.Value.LastRequestDateTime < cutoffTime)
            .Select(pair => pair.Key)
            .ToList();

        var sessionsRemoved = keysToRemove.Count(key => _sessions.TryRemove(key, out _));

        return Task.FromResult(sessionsRemoved);
    }

    public Task ValidateProvidedSecret(string playerName, string secret)
    {
        _sessions.TryGetValue(playerName, out var session);
        if (session != null && !session.ValidateSecret(secret))
        {
            throw new Exception("Invalid secret provided!");
        }
        return Task.CompletedTask;
    }
}