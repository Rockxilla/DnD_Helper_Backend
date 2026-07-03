using DnD_Helper_Backend.DTOs;

namespace DnD_Helper_Backend.Services.Interfaces
{
    public interface IPersonajeCrearService
    {
        Task CreateSkillsInicialesAsync(int personajeId);
        Task CreateScoresInicialesAsync(int personajeId, ScoresDto scoresDto);
        Task CreateStatsInicialesAsync(int personajeId);
    }
}
