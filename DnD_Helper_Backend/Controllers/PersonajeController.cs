using DnD_Helper_Backend.DTOs;
using DnD_Helper_Backend.Interfaces;
using DnD_Helper_Backend.Models;
using DnD_Helper_Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.PortableExecutable;

namespace DnD_Helper_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonajeController : ControllerBase
    {
        private readonly IPersonajeRepository _personajeRepository;
        private readonly ISkillService _skillService;
        public PersonajeController(IPersonajeRepository personajeRepository, ISkillService skillService)
        {
            _personajeRepository = personajeRepository;
            _skillService = skillService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetPersonajesAsync()
        {
            var result = await _personajeRepository.GetPersonajesAsync();
            return Ok(result);
        }

        [HttpGet("lista")]
        public async Task<IActionResult> GetPersonajesListAsync()
        {
            var result = await _personajeRepository.GetPersonajesListAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonajeByIdAsync(int id)
        {
            var result = await _personajeRepository.GetPersonajeByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] CreatePersonajeDto dto)
        {
            var personajeId = await _personajeRepository.CreatePersonajeAsync(dto);
            return Ok(new
            {
                personaje_ID = personajeId,
                mensaje = "Personaje creado correctamente"
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] UpdatePersonajeDto dto)
        {
            var success = await _personajeRepository.UpdatePersonajeAsync(dto);
            return success ? Ok() : NotFound();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonajeAsync(int id)
        {
            var deletedPersonaje = await _personajeRepository.DeletePersonajeAsync(id);
            return deletedPersonaje ? Ok() : NotFound();
        }

        [HttpGet("{id}/raza")]
        public async Task<IActionResult> GetRaza(int id)
        {
            var result = await _personajeRepository.GetPersonajeRazaAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("{id}/clases")]
        public async Task<IActionResult> GetClases(int id)
        {
            var result = await _personajeRepository.GetPersonajeClasesAsync(id);

            return Ok(result);
        }
        [HttpGet("{id}/scores")]
        public async Task<IActionResult> GetScores(int id)
        {
            var result = await _personajeRepository.GetPersonajeScoresAsync(id);

            return Ok(result);
        }
        [HttpGet("{id}/skills")]
        public async Task<IActionResult> GetSkills(int id)
        {
            var result = await _skillService.GetPersonajeSkillsAsync(id);
            return Ok(result);
        }
    }
    }
