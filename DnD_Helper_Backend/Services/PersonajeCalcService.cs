using DnD_Helper_Backend.Models.Enums;
using DnD_Helper_Backend.Models.Instances;
using DnD_Helper_Backend.Services.Interfaces;

namespace DnD_Helper_Backend.Services
{
    public class PersonajeCalcService : IPersonajeCalcService
    {
        // MODIFICADOR DE HABILIDADES
        public int CalcModificadorHabilidad(byte score)
        {
            return (int)Math.Floor((score - 10) / 2.0);
        }
        // PROFICIENCIA BASE
        public int CalcProficiencia(int nivelTotal)
        {
            return nivelTotal switch
            {
                <= 0 => throw new ArgumentException("Nivel no Valido."),

                <= 4 =>     2,
                <= 8 =>     3,
                <= 12 =>    4,
                <= 16 =>    5,
                _ =>        6
            };
        }
        //PROFICIENCIA TOTAL
        public int CalcProficienciaFinal(int nivelTotal, int additionalBonus)
        {
            return CalcProficiencia(nivelTotal) + additionalBonus;
        }
        //NIVELES TOTALES
        public int CalcNivelTotal(IEnumerable<ClasePersonaje> clases)
        {
            return clases.Sum(c => c.Nivel);
        }
        //CALCULAR BONUS DE SKILLS CON PROFICIENCIA
        public int CalcSkillProfBonus(Proficiencias nivel, int proficiencyBonus)
        {
            return nivel switch
            {
                Proficiencias.Nada => 0,
                Proficiencias.Proficiente => proficiencyBonus,
                Proficiencias.Experto => proficiencyBonus * 2,
                Proficiencias.Medio => (int)Math.Floor(proficiencyBonus / 2.0),
                _ => throw new Exception("Proficiencia Invalida")
            };
        }
    }
}
