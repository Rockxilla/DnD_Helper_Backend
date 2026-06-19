using DnD_Helper_Backend.Data;
using DnD_Helper_Backend.DTOs;
using DnD_Helper_Backend.Interfaces;
using DnD_Helper_Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DnD_Helper_Backend.Repositories
{
    public class ClaseRepository : IClaseReopository
    {
        public readonly DnDHelperDBContext _databaseContext;
        public ClaseRepository(DnDHelperDBContext dbContext)
        {
            _databaseContext = dbContext;
        }
        // VER LISTA DE CLASES (Template)
        public async Task<List<ClaseListDto>> GetClasesListAsync()
        {
            return await _databaseContext.ClaseTemplates
                .Select(x => new ClaseListDto
                {
                    ClaseTemplate_ID = x.ClaseTemplate_ID,
                    Nombre = x.Nombre,
                })
                .ToListAsync();
        }

    }
}
