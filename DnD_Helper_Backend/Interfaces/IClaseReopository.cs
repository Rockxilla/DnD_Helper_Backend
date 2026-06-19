using DnD_Helper_Backend.DTOs;
using DnD_Helper_Backend.Models;

namespace DnD_Helper_Backend.Interfaces
{
    public interface IClaseReopository
    {
        Task<List<ClaseListDto>> GetClasesListAsync(); // VER LISTA DE CLASES (Template)
    }
}
