using DnD_Helper_Backend.DTOs;
using DnD_Helper_Backend.Models.Instances;

namespace DnD_Helper_Backend.Services.Interfaces
{
    public interface IPersonajeCrearService
    {
        Task CreateSkillsInicialesAsync(int personajeId);
        Task CreateScoresInicialesAsync(int personajeId, ScoresDto scoresDto);
        void CreateStatsIniciales(int personajeId);
        
        Task<ClasePersonaje> CreateClaseInicialAsync(int personajeId, CreatePersonajeDto dto);
        Task CreateSubclaseInicialAsync(int claseId, CreatePersonajeDto dto);
        Task<RazaPersonaje> CreateRazaInicialAsync(int personajeId, CreatePersonajeDto dto);
        Task CreateSubrazaInicialAsync(int razaId, CreatePersonajeDto dto);

    }
}
