using DnD_Helper_Backend.Models.Enums;
using DnD_Helper_Backend.Models.Instances;

namespace DnD_Helper_Backend.Services.Interfaces
{
    public interface IPersonajeCalcService
    {
        int CalcModificadorHabilidad(byte score);
        int CalcProficiencia(int totalLevel);
        int CalcProficienciaFinal(int totalLevel,int additionalBonus);
        int CalcNivelTotal(IEnumerable<ClasePersonaje> clases);
        int CalcSkillProfBonus(Proficiencias nivel, int proficiencyBonus);
    }
}
