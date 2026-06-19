using DnD_Helper_Backend.Data;
using DnD_Helper_Backend.DTOs;
using DnD_Helper_Backend.Interfaces;
using DnD_Helper_Backend.Models;
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
                    },

                    Clase = x.ClasePersonaje == null ? null : new ClaseDto
                    {
                        ClaseTemplate_ID = x.ClasePersonaje.ClaseTemplate_ID,
                        Nombre = x.ClasePersonaje.Nombre,
                        Descripcion = x.ClasePersonaje.Descripcion
                    },

                    Raza = x.RazaPersonaje == null ? null : new RazaDto
                    {
                        RazaTemplate_ID = x.RazaPersonaje.RazaTemplate_ID,
                        Nombre = x.RazaPersonaje.Nombre,
                        Descripcion = x.RazaPersonaje.Descripcion
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
                    },

                    Clase = x.ClasePersonaje == null ? null : new ClaseDto
                    {
                        ClaseTemplate_ID = x.ClasePersonaje.ClaseTemplate_ID,
                        Nombre = x.ClasePersonaje.Nombre,
                        Descripcion = x.ClasePersonaje.Descripcion
                    },

                    Raza = x.RazaPersonaje == null ? null : new RazaDto
                    {
                        RazaTemplate_ID = x.RazaPersonaje.RazaTemplate_ID,
                        Nombre = x.RazaPersonaje.Nombre,
                        Descripcion = x.RazaPersonaje.Descripcion
                    }
                })
                .FirstOrDefaultAsync();
        }

        //CREAR PERSONAJE
        public async Task<Personaje> CreatePersonajeAsync(CreatePersonajeDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre))
                throw new Exception("Nombre es obligatorio");
            
            if (dto.Experiencia < 0)
                throw new Exception("Experiencia no puede ser negativa");

            if (dto.Nombre.Length > 100)
                throw new Exception("Nombre tiene más de 100 caracteres)");

            var usuarioExists = await _databaseContext.Usuarios.AnyAsync(x => x.Usuario_ID == dto.Usuario_ID);

            var claseTemplate = await _databaseContext.ClaseTemplates.FirstOrDefaultAsync(x => x.ClaseTemplate_ID == dto.ClaseTemplate_ID);

            var razaTemplate = await _databaseContext.RazaTemplates.FirstOrDefaultAsync(x => x.RazaTemplate_ID == dto.RazaTemplate_ID);

            if (!usuarioExists || claseTemplate == null || razaTemplate == null)
                throw new Exception("Usuario, Clase o Raza no válida");

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

            // CREAR CLASE (del Template)
            var clasePersonaje = new ClasePersonaje
            {
                Personaje_ID = personaje.Personaje_ID,
                ClaseTemplate_ID = claseTemplate.ClaseTemplate_ID,
                Nombre = claseTemplate.Nombre,
                Descripcion = claseTemplate.Descripcion,
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

            return personaje;
        }

        //EDITAR PERSONAJE
        public async Task<bool> UpdatePersonajeAsync(UpdatePersonajeDto dto)
        {
            var entity = await _databaseContext.Personajes.Include(x => x.ClasePersonaje).Include(x => x.RazaPersonaje).FirstOrDefaultAsync(x => x.Personaje_ID == dto.Personaje_ID);

            if (entity == null)
                return false;

            if (dto.Experiencia < 0)
                throw new Exception("Experiencia no puede ser negativa");

            if (!string.IsNullOrWhiteSpace(dto.Nombre) && dto.Nombre.Length > 100)
                throw new Exception("Nombre tiene más de 100 caracteres");

            // EDITAR DATOS DEL PERSONAJE
            entity.Nombre = dto.Nombre ?? entity.Nombre;
            entity.Experiencia = dto.Experiencia ?? entity.Experiencia;
            // EDITAR CLASE
            if (entity.ClasePersonaje != null)
            {
                entity.ClasePersonaje.Nombre = dto.ClaseNombre ?? entity.ClasePersonaje.Nombre;
                entity.ClasePersonaje.Descripcion = dto.ClaseDescripcion ?? entity.ClasePersonaje.Descripcion;
            }
            // EDITAR RAZA
            if (entity.RazaPersonaje != null)
            {
                entity.RazaPersonaje.Nombre = dto.RazaNombre ?? entity.RazaPersonaje.Nombre;

                entity.RazaPersonaje.Descripcion = dto.RazaDescripcion ?? entity.RazaPersonaje.Descripcion;
            }

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
        
    }
}
