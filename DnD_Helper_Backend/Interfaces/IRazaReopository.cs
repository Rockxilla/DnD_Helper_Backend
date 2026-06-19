using DnD_Helper_Backend.DTOs;
using DnD_Helper_Backend.Models;

namespace DnD_Helper_Backend.Interfaces
{
    public interface IRazaReopository
    {
        Task<List<RazaListDto>> GetRazasListAsync(); // VER LISTA DE CLASES (Template)
    }
}
