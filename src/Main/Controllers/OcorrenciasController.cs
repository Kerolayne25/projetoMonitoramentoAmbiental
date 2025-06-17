using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fase4Cap7WebserviceASPNET.Main.Data;
using Fase4Cap7WebserviceASPNET.Main.Models;
using Fase4Cap7WebserviceASPNET.Main.ViewModels;

namespace Fase4Cap7WebserviceASPNET.Main.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class OcorrenciasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OcorrenciasController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Cadastra uma nova ocorrência ambiental.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] OcorrenciaViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ocorrencia = new Ocorrencia
            {
                Descricao = model.Descricao,
                DataOcorrencia = model.DataOcorrencia,
                Tipo = model.Tipo,
                TipoImpactoId = model.TipoImpactoId,
                Status = "PENDENTE"
            };

            _context.Ocorrencias.Add(ocorrencia);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = ocorrencia.Id }, ocorrencia);
        }

        /// <summary>
        /// Retorna uma ocorrência pelo ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<OcorrenciaResponseViewModel>> GetById(int id)
        {
            var ocorrencia = await _context.Ocorrencias
                .Include(o => o.TipoImpacto)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (ocorrencia == null)
                return NotFound();

            var viewModel = new OcorrenciaResponseViewModel
            {
                Id = ocorrencia.Id,
                Descricao = ocorrencia.Descricao,
                DataOcorrencia = ocorrencia.DataOcorrencia,
                Tipo = ocorrencia.Tipo,
                Status = ocorrencia.Status,
                TipoImpactoId = ocorrencia.TipoImpactoId,
                TipoImpactoNome = ocorrencia.TipoImpacto?.Nome ?? string.Empty
            };

            return Ok(viewModel);
        }

        /// <summary>
        /// Lista todas as ocorrências com filtros opcionais e paginação.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OcorrenciaResponseViewModel>>> Get(
            [FromQuery] string? status,
            [FromQuery] DateTime? dataInicio,
            [FromQuery] DateTime? dataFim,
            [FromQuery] int? tipoImpactoId,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var query = _context.Ocorrencias
                .Include(o => o.TipoImpacto)
                .AsQueryable();

            if (!string.IsNullOrEmpty(status))
                query = query.Where(o => o.Status == status);

            if (dataInicio.HasValue)
                query = query.Where(o => o.DataOcorrencia >= dataInicio.Value);

            if (dataFim.HasValue)
                query = query.Where(o => o.DataOcorrencia <= dataFim.Value);

            if (tipoImpactoId.HasValue)
                query = query.Where(o => o.TipoImpactoId == tipoImpactoId.Value);

            var resultados = await query
                .OrderByDescending(o => o.DataOcorrencia)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(o => new OcorrenciaResponseViewModel
                {
                    Id = o.Id,
                    Descricao = o.Descricao,
                    DataOcorrencia = o.DataOcorrencia,
                    Tipo = o.Tipo,
                    Status = o.Status,
                    TipoImpactoId = o.TipoImpactoId,
                    TipoImpactoNome = o.TipoImpacto!.Nome
                })
                .ToListAsync();

            return Ok(resultados);
        }

        /// <summary>
        /// Atualiza o status de uma ocorrência.
        /// </summary>
        [HttpPut("{id}/status")]
        public async Task<IActionResult> AtualizarStatus(int id, [FromBody] string novoStatus)
        {
            var ocorrencia = await _context.Ocorrencias.FindAsync(id);
            if (ocorrencia == null)
                return NotFound();

            ocorrencia.Status = novoStatus.ToUpper();
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Exclui uma ocorrência pelo ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ocorrencia = await _context.Ocorrencias.FindAsync(id);
            if (ocorrencia == null)
                return NotFound();

            _context.Ocorrencias.Remove(ocorrencia);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
