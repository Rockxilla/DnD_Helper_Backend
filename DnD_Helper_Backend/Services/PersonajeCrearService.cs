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

        //CREAR CLASE INICIAL
        public async Task CreateClaseInicialAsync(int personajeId, CreatePersonajeDto dto)
        {
            ClasePersonaje clase = new()
            {
                Personaje_ID = personajeId,
                Nivel = dto.ClaseNivelInicial,
                Estatus = true
            };

            if (dto.ClaseTemplate_ID.HasValue)
            {
                var template = await _databaseContext.ClaseTemplates.FirstOrDefaultAsync(x => x.ClaseTemplate_ID == dto.ClaseTemplate_ID);

                if (template == null)
                    throw new Exception("Clase inválida");

                clase.ClaseTemplate_ID = template.ClaseTemplate_ID;
                clase.Nombre = string.IsNullOrWhiteSpace(dto.ClaseNombre)? template.Nombre : dto.ClaseNombre.Trim();
                clase.Descripcion = string.IsNullOrWhiteSpace(dto.ClaseDescripcion) ? template.Descripcion : dto.ClaseDescripcion.Trim();
                clase.Hit_Dice_ID = dto.HitDice_ID ?? template.Hit_Dice_ID;
            }
            else
            {
                //Sin Template
                clase.Nombre = string.IsNullOrWhiteSpace(dto.ClaseNombre) ? null : dto.ClaseNombre.Trim();
                clase.Descripcion = string.IsNullOrWhiteSpace(dto.ClaseDescripcion) ? null : dto.ClaseDescripcion.Trim();
                clase.Hit_Dice_ID = dto.HitDice_ID ?? 1;
            }

            _databaseContext.ClasePersonajes.Add(clase);
        }
        
        //CREAR RAZA INICIAL
        public async Task CreateRazaInicialAsync(int personajeId, CreatePersonajeDto dto)
        {
            RazaPersonaje raza = new()
            {
                Personaje_ID = personajeId,
                Estatus = true
            };

            if (dto.RazaTemplate_ID.HasValue)
            {
                //Con Template
                var template = await _databaseContext.RazaTemplates.FirstOrDefaultAsync(x => x.RazaTemplate_ID == dto.RazaTemplate_ID);

                if (template == null)
                    throw new Exception("Raza inválida");

                raza.RazaTemplate_ID = template.RazaTemplate_ID;
                raza.Nombre = string.IsNullOrWhiteSpace(dto.RazaNombre) ? template.Nombre : dto.RazaNombre.Trim();
                raza.Descripcion = string.IsNullOrWhiteSpace(dto.RazaDescripcion) ? template.Descripcion : dto.RazaDescripcion.Trim();
            }
            else
            {
                //Sin Template
                raza.Nombre = string.IsNullOrWhiteSpace(dto.RazaNombre) ? null : dto.RazaNombre.Trim();
                raza.Descripcion = string.IsNullOrWhiteSpace(dto.RazaDescripcion) ? null : dto.RazaDescripcion.Trim();
            }

            _databaseContext.RazaPersonajes.Add(raza);
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
        }

        // CREAR STATS INICIALES
        public void CreateStatsIniciales(int personajeId)
        {
            _databaseContext.StatsPersonajes.Add(new StatsPersonaje
            {
                Personaje_ID = personajeId
            });
        }
    }
}
