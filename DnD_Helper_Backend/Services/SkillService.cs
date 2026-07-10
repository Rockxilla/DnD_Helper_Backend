using DnD_Helper_Backend.Data;
using DnD_Helper_Backend.DTOs;
using DnD_Helper_Backend.Models.Enums;
using DnD_Helper_Backend.Models.Instances;
using DnD_Helper_Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DnD_Helper_Backend.Services
{
    public class SkillService : ISkillService
    {
        public readonly DnDHelperDBContext _databaseContext;
        private readonly IPersonajeCalcService _calc;
        public SkillService(DnDHelperDBContext databaseContext, IPersonajeCalcService calc)
        {
            _databaseContext = databaseContext;
            _calc = calc;
        }

        public async Task<List<SkillDisplayDto>> GetPersonajeSkillsAsync(int personajeId)
        {
            //ADQUISICION DE DATOS
            var skills = await _databaseContext.SkillPersonajes.Where(x => x.Personaje_ID == personajeId).Include(x => x.SkillTemplate)
                .ThenInclude(t => t!.Habilidad).Include(x => x.SkillCustom).ThenInclude(c => c!.Habilidad).ToListAsync();

            var scores = await _databaseContext.ScorePersonajes.Where(x => x.Personaje_ID == personajeId).ToListAsync();

            var stats = await _databaseContext.StatsPersonajes.FirstOrDefaultAsync(x => x.Personaje_ID == personajeId);

            if (stats == null)
                throw new Exception("No hay Stats");

            var scoreLookup = scores.ToDictionary(x => x.Habilidad_ID);

            // CALCULAR NIVEL TOTAL
            var clases = await _databaseContext.ClasePersonajes.Where(x => x.Personaje_ID == personajeId).ToListAsync();

            var nivelTotal = _calc.CalcNivelTotal(clases);

            var proficiencyBonus = _calc.CalcProficienciaFinal(nivelTotal, stats.ProficiencyBonus);

            return skills.Select(skill => MapSkill(skill, scoreLookup, proficiencyBonus)).ToList();
        }

        private SkillDisplayDto MapSkill(SkillPersonaje skill, Dictionary<byte, ScorePersonaje> scoreLookup, int proficiencyBonus)
        {
            var nombre = skill.SkillTemplate != null ? skill.SkillTemplate.Nombre : skill.SkillCustom!.Nombre;

            var habilidadNombre = skill.SkillTemplate != null ? skill.SkillTemplate.Habilidad.NombreCorto : skill.SkillCustom!.Habilidad.NombreCorto;

            var habilidadId = skill.SkillTemplate != null ? skill.SkillTemplate.Habilidad_ID : skill.SkillCustom!.Habilidad_ID;

            var score = scoreLookup[habilidadId];

            var modifier = _calc.CalcModificadorHabilidad(score.ValorBase);

            var bonusProficiency = _calc.CalcSkillProfBonus((Proficiencias)skill.Proficiencia, proficiencyBonus);

            var total = modifier + bonusProficiency + skill.BonusTemporal;

            return new SkillDisplayDto
            {
                SkillPersonaje_ID = skill.SkillPersonaje_ID,
                Nombre = nombre,
                Habilidad_ID = habilidadId,
                Habilidad = habilidadNombre,
                ValorHabilidad = score.ValorBase,
                ModificadorHabilidad = modifier,
                Proficiencia = skill.Proficiencia,
                BonusProficiencia = bonusProficiency,
                BonusTemporal = skill.BonusTemporal,
                BonusTotal = total
            };
        }
    }
}
