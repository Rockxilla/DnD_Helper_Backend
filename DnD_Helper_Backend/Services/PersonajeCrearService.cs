using DnD_Helper_Backend.Data;
using DnD_Helper_Backend.DTOs;
using DnD_Helper_Backend.Models.Instances;
using DnD_Helper_Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DnD_Helper_Backend.Services
{
    public class PersonajeCrearService : IPersonajeCrearService
    {
        public readonly DnDHelperDBContext _databaseContext;
        public PersonajeCrearService(DnDHelperDBContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        //CREAR SKILLS INICIALES
        public async Task CreateSkillsInicialesAsync(int personajeId)
        {
            var templates = await _databaseContext.SkillTemplates.ToListAsync();

            var skills = templates.Select(template => new SkillPersonaje
            {
                Personaje_ID = personajeId,
                SkillTemplate_ID = template.SkillTemplate_ID,
                Proficiencia = 0,
                BonusTemporal = 0
            });

            _databaseContext.SkillPersonajes.AddRange(skills);

            await _databaseContext.SaveChangesAsync();
        }
        //CREAR SCORES DE HABILIDAD INICIALES
        public async Task CreateScoresInicialesAsync(int personajeId, ScoresDto scoresDto)
        {
            var habilidades = await _databaseContext.Habilidades.ToListAsync();
            
            var ids = habilidades.ToDictionary(h => h.NombreCorto,h => h.Habilidad_ID);

            var scoreValues = new Dictionary<string, byte>
            {
                ["STR"] = scoresDto.STR,
                ["DEX"] = scoresDto.DEX,
                ["CON"] = scoresDto.CON,
                ["INT"] = scoresDto.INT,
                ["WIS"] = scoresDto.WIS,
                ["CHA"] = scoresDto.CHA
            };

            var scores = scoreValues.Select(pair => new ScorePersonaje
            {
                Personaje_ID = personajeId,
                Habilidad_ID = ids[pair.Key],
                ValorBase = pair.Value,
                BonusTemporal = 0,
                EsProficiente = false
            }).ToList();

            _databaseContext.ScorePersonajes.AddRange(scores);
            await _databaseContext.SaveChangesAsync();
        }

        // CREAR STATS INICIALES
        public async Task CreateStatsInicialesAsync(int personajeId)
        {
            _databaseContext.StatsPersonajes.Add(new StatsPersonaje
            {
                Personaje_ID = personajeId
            });

            await _databaseContext.SaveChangesAsync();
        }
    }
}
