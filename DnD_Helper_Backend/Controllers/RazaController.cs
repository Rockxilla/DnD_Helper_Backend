using DnD_Helper_Backend.DTOs;
using DnD_Helper_Backend.Interfaces;
using DnD_Helper_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.PortableExecutable;

namespace DnD_Helper_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RazaController : ControllerBase
    {
        private readonly IRazaReopository _razajeRepository;
        public RazaController(IRazaReopository claseReopository)
        {
            _razajeRepository = claseReopository;
        }


        [HttpGet("GetRazaLista")]
        public async Task<IActionResult> GetRazasListAsync()
        {
            var result = await _razajeRepository.GetRazasListAsync();
            return Ok(result);
        }
    }
}
