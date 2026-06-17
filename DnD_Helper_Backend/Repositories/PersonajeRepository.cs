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

                    Clase = x.Clase == null ? null : new ClaseDto
                    {
                        Clase_ID = x.Clase.Clase_ID,
                        Nombre = x.Clase.Nombre
                    },

                    Raza = x.Raza == null ? null : new RazaDto
                    {
                        Raza_ID = x.Raza.Raza_ID,
                        Nombre = x.Raza.Nombre
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

                    Clase = x.Clase == null ? null : new ClaseDto
                    {
                        Clase_ID = x.Clase.Clase_ID,
                        Nombre = x.Clase.Nombre
                    },

                    Raza = x.Raza == null ? null : new RazaDto
                    {
                        Raza_ID = x.Raza.Raza_ID,
                        Nombre = x.Raza.Nombre
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

            if (dto.Usuario_ID <= 0 || dto.Clase_ID <= 0 || dto.Raza_ID <= 0)
                throw new Exception("Usuario, Clase o Raza no elegida");

            var usuarioExists = await _databaseContext.Usuarios.AnyAsync(x => x.Usuario_ID == dto.Usuario_ID);
            var claseExists = await _databaseContext.Clases.AnyAsync(x => x.Clase_ID == dto.Clase_ID);
            var razaExists = await _databaseContext.Razas.AnyAsync(x => x.Raza_ID == dto.Raza_ID);

            if (!usuarioExists || !claseExists || !razaExists)
                throw new Exception("Usuario, Clase o Raza no elegido no existente");

            var entity = new Personaje
            {
                Nombre = dto.Nombre.Trim(),
                Experiencia = dto.Experiencia ?? 0,
                Usuario_ID = dto.Usuario_ID,
                Clase_ID = dto.Clase_ID,
                Raza_ID = dto.Raza_ID,
                
                Estatus = true
            };

            _databaseContext.Personajes.Add(entity);
            await _databaseContext.SaveChangesAsync();

            return entity;
        }

        //EDITAR PERSONAJE
        public async Task<bool> UpdatePersonajeAsync(UpdatePersonajeDto dto)
        {
            var entity = await _databaseContext.Personajes
                .FirstOrDefaultAsync(x => x.Personaje_ID == dto.Personaje_ID);
            
            if (entity == null)
                return false;

            if (dto.Experiencia < 0)
                throw new Exception("Experiencia no puede ser negativa");

            if (dto.Nombre.Length > 100)
                throw new Exception("Nombre tiene más de 100 caracteres)");

            if (dto.Usuario_ID == 0 || dto.Clase_ID == 0 || dto.Raza_ID == 0)
                return false;

            entity.Nombre = dto.Nombre ?? entity.Nombre;
            entity.Experiencia = dto.Experiencia ?? entity.Experiencia;
            entity.Usuario_ID = dto.Usuario_ID;
            entity.Clase_ID = dto.Clase_ID;
            entity.Raza_ID = dto.Raza_ID;

            await _databaseContext.SaveChangesAsync();

            return true;
        }

        //BORRAR PERSONAJE
        public async Task<bool> DeletePersonajeAsync(int id)
        {
            var entity = await _databaseContext.Personajes
                .FirstOrDefaultAsync(x => x.Personaje_ID == id);

            if (entity == null)
                return false;

            entity.Estatus = false;

            await _databaseContext.SaveChangesAsync();

            return true;
        }
        
    }
}
