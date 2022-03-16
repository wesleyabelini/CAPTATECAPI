#nullable disable
using CAPTATECAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Entity;

namespace CAPTATECAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly CAPTATECContext _context;

        public ClientesController(CAPTATECContext context)
        {
            _context = context;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            string StoredProc = "exec SelectAllClient";
            return await _context.Clientes.FromSqlRaw(StoredProc).ToListAsync();
        }

        // GET: api/Clientes/5
        [HttpGet("{cpf}")]
        public async Task<ActionResult<Cliente>> GetCliente(string cpf)
        {
            string StoredProc = "exec SelectAClient " +
                "@CPF = '" + cpf + "'";

            var cliente = await _context.Clientes.FromSqlRaw(StoredProc).ToListAsync();

            if (cliente == null) return NotFound();

            return cliente.First();
        }

        // PUT: api/Clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, Cliente cliente)
        {
            if (ClienteCheck(cliente))
            {
                string SelectCpf = "exec SelectAClient " +
                    "@CPF = '" + cliente.Cpf + "'";

                string StoredProc = "exec EditClient " +
                    "@ClienteId = " + id + ", " +
                    "@Nome = '" + cliente.Nome + "'," +
                    "@CPF = '" + cliente.Cpf + "'," +
                    "@TIPO_CLI = " + cliente.TipoCli + "," +
                    "@SEXO = '" + cliente.Sexo + "'," +
                    "@SIT_CLI = '" + cliente.SitCli + "'";

                try
                {
                    var cli = await _context.Clientes.FromSqlRaw(SelectCpf).ToListAsync();

                    if (cli.Count == 0 || cli.First().Clienteid == id)
                    {
                        var result = await _context.Clientes.FromSqlRaw(StoredProc).ToListAsync();
                        if (result.Count == 1) return Created("https://localhost:7296/api/clientes/" + id, result);
                    }
                    else return Conflict(cli);
                    
                }
                catch (DbUpdateConcurrencyException) { return BadRequest(); }
            }
            
            return BadRequest();
        }

        // POST: api/Clientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            if (ClienteCheck(cliente))
            {
                string StoredProc = "exec InsertClient " +
                    "@Nome = '" + cliente.Nome + "'," +
                    "@CPF = '" + cliente.Cpf + "'," +
                    "@TIPO_CLI = " + cliente.TipoCli + "," +
                    "@SEXO = '" + cliente.Sexo + "'," +
                    "@SIT_CLI = " + cliente.SitCli;

                var result = await _context.Clientes.FromSqlRaw(StoredProc).ToListAsync();
                if(!CheckCliente(cliente, result.First())) return Conflict(result);
                else if(result.Count == 1) return Created("https://localhost:7296/api/clientes", result);
            }

            return BadRequest();
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{cpf}")]
        public async Task<IActionResult> DeleteCliente(string cpf)
        {
            string StoredProcDelete = "exec DeleteClient " +
                "@CPF = '" + cpf + "'";

            var result = await _context.Clientes.FromSqlRaw(StoredProcDelete).ToListAsync();
            if(result.Count == 1) { return Ok(); }

            return NotFound();
        }

        private bool ClienteCheck(Cliente cli)
        {
            if (cli == null) return false;
            if (String.IsNullOrWhiteSpace(cli.Nome)) return false;
            if (String.IsNullOrEmpty(cli.Cpf)) return false;
            if (cli.TipoCli == null || cli.TipoCli == 0) return false;
            if (cli.SitCli == null || cli.SitCli == 0) return false;

            return true;
        }

        private bool CheckCliente(Cliente cliIn, Cliente cliOut)
        {
            if(cliIn.Nome != cliOut.Nome) return false;
            if(cliIn.Cpf != cliOut.Cpf) return false;
            if(cliIn.SitCli != cliOut.SitCli) return false;
            if(cliIn.TipoCli != cliOut.TipoCli) return false;
            if(cliIn.Sexo != cliOut.Sexo) return false;

            return true;
        }
    }
}
