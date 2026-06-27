using DnD_Helper_Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
