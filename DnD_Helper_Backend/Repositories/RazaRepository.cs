using DnD_Helper_Backend.Data;
using DnD_Helper_Backend.DTOs;
using DnD_Helper_Backend.Interfaces;
using DnD_Helper_Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DnD_Helper_Backend.Repositories
{
    public class RazaRepository : IRazaReopository
    {
        public readonly DnDHelperDBContext _databaseContext;
        public RazaRepository(DnDHelperDBContext dbContext)
        {
            _databaseContext = dbContext;
        }
        // VER LISTA DE RAZAS (Template)
        public async Task<List<RazaListDto>> GetRazasListAsync()
        {
            return await _databaseContext.RazaTemplates
                .Select(x => new RazaListDto
                {
                    RazaTemplate_ID = x.RazaTemplate_ID,
                    Nombre = x.Nombre,
                })
                .ToListAsync();
        }

    }
}
