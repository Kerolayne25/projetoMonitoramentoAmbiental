using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fase4Cap7WebserviceASPNET.Main.Data;
using Fase4Cap7WebserviceASPNET.Main.Models;

namespace Fase4Cap7WebserviceASPNET.Main.controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImpactosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ImpactosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoImpacto>>> Get(
            [FromQuery] int page = 1, 
            [FromQuery] int pageSize = 10)
        {
            var query = _context.TiposImpacto
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            var resultados = await query.ToListAsync();
            return Ok(resultados);
        }
    }
}
