#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CAPTATECAPI.Models;
using Entity;

namespace CAPTATECAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoClientesController : ControllerBase
    {
        private readonly CAPTATECContext _context;

        public TipoClientesController(CAPTATECContext context)
        {
            _context = context;
        }

        // GET: api/TipoClientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoCliente>>> GetTipoClientes()
        {
            return await _context.TipoClientes.ToListAsync();
        }

        //// GET: api/TipoClientes/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<TipoCliente>> GetTipoCliente(int id)
        //{
        //    var tipoCliente = await _context.TipoClientes.FindAsync(id);

        //    if (tipoCliente == null)
        //    {
        //        return NotFound();
        //    }

        //    return tipoCliente;
        //}

        //// PUT: api/TipoClientes/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTipoCliente(int id, TipoCliente tipoCliente)
        //{
        //    if (id != tipoCliente.Tipocliid)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(tipoCliente).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TipoClienteExists(id))
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

        //// POST: api/TipoClientes
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<TipoCliente>> PostTipoCliente(TipoCliente tipoCliente)
        //{
        //    _context.TipoClientes.Add(tipoCliente);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetTipoCliente", new { id = tipoCliente.Tipocliid }, tipoCliente);
        //}

        //// DELETE: api/TipoClientes/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTipoCliente(int id)
        //{
        //    var tipoCliente = await _context.TipoClientes.FindAsync(id);
        //    if (tipoCliente == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.TipoClientes.Remove(tipoCliente);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool TipoClienteExists(int id)
        //{
        //    return _context.TipoClientes.Any(e => e.Tipocliid == id);
        //}
    }
}
