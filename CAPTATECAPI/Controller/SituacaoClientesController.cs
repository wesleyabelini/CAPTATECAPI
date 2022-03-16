#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CAPTATECAPI.Models;
using Entity;

namespace CAPTATECAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SituacaoClientesController : ControllerBase
    {
        private readonly CAPTATECContext _context;

        public SituacaoClientesController(CAPTATECContext context)
        {
            _context = context;
        }

        // GET: api/SituacaoClientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SituacaoCliente>>> GetSituacaoClientes()
        {
            return await _context.SituacaoClientes.ToListAsync();
        }

        //// GET: api/SituacaoClientes/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<SituacaoCliente>> GetSituacaoCliente(int id)
        //{
        //    var situacaoCliente = await _context.SituacaoClientes.FindAsync(id);

        //    if (situacaoCliente == null)
        //    {
        //        return NotFound();
        //    }

        //    return situacaoCliente;
        //}

        //// PUT: api/SituacaoClientes/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutSituacaoCliente(int id, SituacaoCliente situacaoCliente)
        //{
        //    if (id != situacaoCliente.Situacaocliid)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(situacaoCliente).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!SituacaoClienteExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/SituacaoClientes
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<SituacaoCliente>> PostSituacaoCliente(SituacaoCliente situacaoCliente)
        //{
        //    _context.SituacaoClientes.Add(situacaoCliente);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetSituacaoCliente", new { id = situacaoCliente.Situacaocliid }, situacaoCliente);
        //}

        //// DELETE: api/SituacaoClientes/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteSituacaoCliente(int id)
        //{
        //    var situacaoCliente = await _context.SituacaoClientes.FindAsync(id);
        //    if (situacaoCliente == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.SituacaoClientes.Remove(situacaoCliente);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool SituacaoClienteExists(int id)
        //{
        //    return _context.SituacaoClientes.Any(e => e.Situacaocliid == id);
        //}
    }
}
