using DnD_Helper_Backend.DTOs;
using DnD_Helper_Backend.Models;

namespace DnD_Helper_Backend.Interfaces
{
    public interface IPersonajeRepository
    {
        Task<List<PersonajeDto>> GetPersonajesAsync();  //Toda la Info
        Task<List<PersonajeListDto>> GetPersonajesListAsync(); // Solo Nombres y XP
        Task<Personaje> CreatePersonajeAsync(CreatePersonajeDto dto);
        Task<bool> UpdatePersonajeAsync(UpdatePersonajeDto dto);
        Task<bool> DeletePersonajeAsync(int id);

        Task<Personaje> GetPersonajeById(int id);

    }
}
