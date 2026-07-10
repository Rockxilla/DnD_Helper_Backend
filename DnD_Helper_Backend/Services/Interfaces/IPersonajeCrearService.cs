using DnD_Helper_Backend.DTOs;

namespace DnD_Helper_Backend.Services.Interfaces
{
    public interface IPersonajeCrearService
    {
        Task CreateSkillsInicialesAsync(int personajeId);
        Task CreateScoresInicialesAsync(int personajeId, ScoresDto scoresDto);
        void CreateStatsIniciales(int personajeId);
        Task CreateClaseInicialAsync(int personajeId, CreatePersonajeDto dto);
        Task CreateRazaInicialAsync(int personajeId, CreatePersonajeDto dto);
    }
}
