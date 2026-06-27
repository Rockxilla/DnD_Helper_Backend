using DnD_Helper_Backend.Data;
using DnD_Helper_Backend.DTOs;
using DnD_Helper_Backend.Interfaces;
using DnD_Helper_Backend.Models.Instances;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DnD_Helper_Backend.Repositories
{
    public class PersonajeRepository : IPersonajeRepository
    {
        public readonly DnDHelperDBContext _databaseContext;
        public PersonajeRepository(DnDHelperDBContext dbContext)
        {
            _databaseContext = dbContext;
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
                })
                .ToListAsync();
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
                })
                .ToListAsync();
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
                })
                .FirstOrDefaultAsync();
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

            var claseTemplate = await _databaseContext.ClaseTemplates.FindAsync(dto.ClaseTemplate_ID);

            var razaTemplate = await _databaseContext.RazaTemplates.FindAsync(dto.RazaTemplate_ID);

            if (!usuarioExists || claseTemplate == null || razaTemplate == null)
                throw new Exception("Usuario, Clase o Raza no válida");

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

                Console.WriteLine(personaje.Personaje_ID);
                // CREAR CLASE (del Template)
                var clasePersonaje = new ClasePersonaje
                {
                    Personaje_ID = personaje.Personaje_ID,
                    ClaseTemplate_ID = claseTemplate.ClaseTemplate_ID,
                    Nombre = claseTemplate.Nombre,
                    Descripcion = claseTemplate.Descripcion,
                    Nivel = dto.ClaseNivelInicial,
                    Hit_Dice_ID = claseTemplate.Hit_Dice_ID,
                    Estatus = true
                };

                // CREAR RAZA (del Template)
                var razaPersonaje = new RazaPersonaje
                {
                    Personaje_ID = personaje.Personaje_ID,
                    RazaTemplate_ID = razaTemplate.RazaTemplate_ID,
                    Nombre = razaTemplate.Nombre,
                    Descripcion = razaTemplate.Descripcion,
                    Estatus = true
                };
                
                _databaseContext.ClasePersonajes.Add(clasePersonaje);
                _databaseContext.RazaPersonajes.Add(razaPersonaje);

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
            return await _databaseContext.RazaPersonajes
                .Where(x => x.Personaje_ID == personajeId)
                .Select(x => new RazaPersonajeDto
                {
                    RazaTemplate_ID = x.RazaTemplate_ID,
                    Nombre = x.Nombre,
                    Descripcion = x.Descripcion
                })
                .FirstOrDefaultAsync();
        }
        // GET CLASES DEL PERSONAJE
        public async Task<List<GetClasePersonajeDto>> GetPersonajeClasesAsync(int personajeId)
        {
            return await _databaseContext.ClasePersonajes
                .Where(x => x.Personaje_ID == personajeId)
                .Select(x => new GetClasePersonajeDto
                {
                    ClaseTemplate_ID = x.ClaseTemplate_ID,
                    Nombre = x.Nombre,
                    Descripcion = x.Descripcion,
                    Nivel = x.Nivel,
                    Hit_Dice_ID = x.Hit_Dice_ID
                })
                .ToListAsync();
        }
    }
}
