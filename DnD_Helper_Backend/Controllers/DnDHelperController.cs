using DnD_Helper_Backend.DTOs;
using DnD_Helper_Backend.Interfaces;
using DnD_Helper_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.PortableExecutable;

namespace DnD_Helper_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonajeController : ControllerBase
    {
        private readonly IPersonajeRepository _personajeRepository;
        public PersonajeController(IPersonajeRepository personajeRepository)
        {
            _personajeRepository = personajeRepository;
        }

        [HttpGet("GetPersonaje")]
        public async Task<IActionResult> GetPersonajesAsync()
        {
            var result = await _personajeRepository.GetPersonajesAsync();
            return Ok(result);
        }

        [HttpGet("GetPersonajeLista")]
        public async Task<IActionResult> GetPersonajesListAsync()
        {
            var result = await _personajeRepository.GetPersonajesListAsync();
            return Ok(result);
        }

        [HttpPost("CreatePersonaje")]
        public async Task<IActionResult> Create(CreatePersonajeDto dto)
        {
            var created = await _personajeRepository.CreatePersonajeAsync(dto);
            return Ok(created);
        }

        [HttpPut("UpdatePersonaje")]
        public async Task<IActionResult> Update(UpdatePersonajeDto dto)
        {
            var success = await _personajeRepository.UpdatePersonajeAsync(dto);
            return success ? Ok() : NotFound();
        }
        [HttpDelete("DeletePersonaje")]
        public async Task<IActionResult> DeletePersonajeAsync(int id)
        {
            var deletedPersonaje = await _personajeRepository.DeletePersonajeAsync(id);
            return Ok(deletedPersonaje);
        }
    }
    }
