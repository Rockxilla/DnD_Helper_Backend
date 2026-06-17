using DnD_Helper_Backend.DTOs;
using DnD_Helper_Backend.Models;

namespace DnD_Helper_Backend.Interfaces
{
    public interface IPersonajeRepository
    {
        Task<List<PersonajeDto>> GetPersonajesAsync(); //VER TODOS PERSONAJES
        Task<List<PersonajeListDto>> GetPersonajesListAsync(); // VER TODOS PERSONAJES, SOLO NOMBRE Y XP
        Task<PersonajeDto?> GetPersonajeByIdAsync(int id); //VER INFO 1 SOLO PERSONAJE
        Task<Personaje> CreatePersonajeAsync(CreatePersonajeDto dto);
        Task<bool> UpdatePersonajeAsync(UpdatePersonajeDto dto);
        Task<bool> DeletePersonajeAsync(int id);

    }
}
