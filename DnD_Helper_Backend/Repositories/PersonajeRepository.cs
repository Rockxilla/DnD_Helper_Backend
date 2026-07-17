using DnD_Helper_Backend.Data;
using DnD_Helper_Backend.DTOs;
using DnD_Helper_Backend.Interfaces;
using DnD_Helper_Backend.Models.Instances;
using DnD_Helper_Backend.Services;
using DnD_Helper_Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DnD_Helper_Backend.Repositories
{
    public class PersonajeRepository : IPersonajeRepository
    {
        public readonly DnDHelperDBContext _databaseContext;
        private readonly IPersonajeCrearService _personajeCrearService;
        private readonly ISkillService _skillService;

        public PersonajeRepository(DnDHelperDBContext dbContext, IPersonajeCrearService personajeCrearService, ISkillService skillService)
        {
            _databaseContext = dbContext;

            _personajeCrearService = personajeCrearService;

            _skillService = skillService;
        }
        //VER TODOS PERSONAJES
        public async Task<List<PersonajeDto>> GetPersonajesAsync()
        {
            return await _databaseContext.Personajes
                .Select(x => new PersonajeDto
                {
                    Personaje_ID = x.Personaje_ID,
                    Nombre = x.Nombre,
                    Experiencia = x.Experiencia,

                    Usuario = x.Usuario == null ? null : new UsuarioDto
                    {
                        Usuario_ID = x.Usuario.Usuario_ID,
                        Nombre = x.Usuario.Nombre
                    }
                }).ToListAsync();
        }
        // VER PERSONAJES, LISTA CORTA
        public async Task<List<PersonajeListDto>> GetPersonajesListAsync()
        {
            return await _databaseContext.Personajes
                .Select(x => new PersonajeListDto
                {
                    Personaje_ID = x.Personaje_ID,
                    Nombre = x.Nombre,
                    Experiencia = x.Experiencia
                }).ToListAsync();
        }
        //VER 1 SOLO PERSONAJE
        public async Task<PersonajeDto?> GetPersonajeByIdAsync(int id)
        {
            return await _databaseContext.Personajes
                .Where(x => x.Personaje_ID == id)
                .Select(x => new PersonajeDto
                {
                    Personaje_ID = x.Personaje_ID,
                    Nombre = x.Nombre,
                    Experiencia = x.Experiencia,

                    Usuario = x.Usuario == null ? null : new UsuarioDto
                    {
                        Usuario_ID = x.Usuario.Usuario_ID,
                        Nombre = x.Usuario.Nombre
                    }
                }).FirstOrDefaultAsync();
        }
        //CREAR PERSONAJE
        public async Task<int> CreatePersonajeAsync(CreatePersonajeDto dto)
        {
            // VALIDACIONES
            if (string.IsNullOrWhiteSpace(dto.Nombre))
                throw new Exception("Nombre es obligatorio");
            
            if (dto.Experiencia < 0)
                throw new Exception("Experiencia no puede ser negativa");

            if (dto.Nombre.Length > 100)
                throw new Exception("Nombre tiene más de 100 caracteres)");
            
            if (dto.ClaseNivelInicial < 1)
                throw new Exception("Nivel inicial inválido");

            var usuarioExists = await _databaseContext.Usuarios.AnyAsync(x => x.Usuario_ID == dto.Usuario_ID);

            if (!usuarioExists)
                throw new Exception("Usuario no válido");

            using var transaction = await _databaseContext.Database.BeginTransactionAsync();
            try
            {
                // CREAR PERSONAJE
                var personaje = new Personaje
                {
                    Nombre = dto.Nombre.Trim(),
                    Experiencia = dto.Experiencia ?? 0,
                    Usuario_ID = dto.Usuario_ID,
                    Estatus = true
                };
                _databaseContext.Personajes.Add(personaje);
                await _databaseContext.SaveChangesAsync();

                // CREAR SCORES DE HABILIDAD
                await _personajeCrearService.CreateScoresInicialesAsync(personaje.Personaje_ID, dto.Scores);
                // CREAR SKILLS
                await _personajeCrearService.CreateSkillsInicialesAsync(personaje.Personaje_ID);
                // CREAR STATS INICIALES
                _personajeCrearService.CreateStatsIniciales(personaje.Personaje_ID);
                Console.WriteLine(personaje.Personaje_ID);
                
                // CREAR CLASE
                var clase = await _personajeCrearService.CreateClaseInicialAsync(personaje.Personaje_ID, dto);
                // CREAR RAZA
                var raza = await _personajeCrearService.CreateRazaInicialAsync(personaje.Personaje_ID, dto);
                //GUARDAR CLASE Y RAZA
                await _databaseContext.SaveChangesAsync();
                
                // CREAR SUBCLASE
                await _personajeCrearService.CreateSubclaseInicialAsync(clase.ClasePersonaje_ID, dto);
                // CREAR SUBRAZA
                await _personajeCrearService.CreateSubrazaInicialAsync(raza.RazaPersonaje_ID, dto);

                //GUARDAR TODO
                await _databaseContext.SaveChangesAsync();

                await transaction.CommitAsync();

                return personaje.Personaje_ID;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        //EDITAR PERSONAJE
        public async Task< bool> UpdatePersonajeAsync(UpdatePersonajeDto dto)
        {
            var entity = await _databaseContext.Personajes.FirstOrDefaultAsync(x => x.Personaje_ID == dto.Personaje_ID);

            if (entity == null)
                return false;

            if (dto.Experiencia < 0)
                throw new Exception("Experiencia no puede ser negativa");

            if (!string.IsNullOrWhiteSpace(dto.Nombre) && dto.Nombre.Length > 100)
                throw new Exception("Nombre tiene más de 100 caracteres");

            // EDITAR DATOS DEL PERSONAJE
            entity.Nombre = dto.Nombre ?? entity.Nombre;
            entity.Experiencia = dto.Experiencia ?? entity.Experiencia;

            await _databaseContext.SaveChangesAsync();
            return true;
        }
        //BORRAR PERSONAJE
        public async Task<bool> DeletePersonajeAsync(int id)
        {
            var entity = await _databaseContext.Personajes.FirstOrDefaultAsync(x => x.Personaje_ID == id);

            if (entity == null)
                return false;

            entity.Estatus = false;

            await _databaseContext.SaveChangesAsync();

            return true;
        }

        // GET RAZA DEL PERSONAJE
        public async Task<RazaPersonajeDto?> GetPersonajeRazaAsync(int personajeId)
        {
            return await _databaseContext.RazaPersonajes.Where(x => x.Personaje_ID == personajeId).Select(x => new RazaPersonajeDto
                {
                    RazaTemplate_ID = x.RazaTemplate_ID,
                    Nombre = x.Nombre,
                    Descripcion = x.Descripcion
                }).FirstOrDefaultAsync();
        }
        // GET CLASES DEL PERSONAJE
        public async Task<List<GetClasePersonajeDto>> GetPersonajeClasesAsync(int personajeId)
        {
            return await _databaseContext.ClasePersonajes.Where(x => x.Personaje_ID == personajeId).Select(x => new GetClasePersonajeDto
                {
                    ClaseTemplate_ID = x.ClaseTemplate_ID,
                    Nombre = x.Nombre,
                    Descripcion = x.Descripcion,
                    Nivel = x.Nivel,
                    Hit_Dice_ID = x.Hit_Dice_ID
                }).ToListAsync();
        }
        // GET SCORES DEL PERSONAJE
        public async Task<List<GetScorePersonajeDto>> GetPersonajeScoresAsync(int personajeId)
        {
            return await _databaseContext.ScorePersonajes.Where(x => x.Personaje_ID == personajeId).Select(x => new GetScorePersonajeDto
                {
                    Habilidad_ID = x.Habilidad_ID,
                    NombreCorto = x.Habilidad.NombreCorto,
                    Nombre = x.Habilidad.Nombre,
                    ValorBase = x.ValorBase,
                    BonusTemporal = x.BonusTemporal,
                    EsProficiente = x.EsProficiente
                }).ToListAsync();
        }
    }
}
