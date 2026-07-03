using DnD_Helper_Backend.DTOs;

namespace DnD_Helper_Backend.Services.Interfaces
{
    public interface ISkillService
    {
        Task<List<SkillDisplayDto>> GetPersonajeSkillsAsync(int personajeId);

    }
}
