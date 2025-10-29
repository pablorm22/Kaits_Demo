using Microsoft.AspNetCore.Mvc;
using Kaits.Application.Interfaces;
using Kaits.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kaits.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IKaitsDbContext _context;

        public ClientesController(IKaitsDbContext context)
        {
            _context = context;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clientes = await _context.Clientes.ToListAsync();
            return Ok(clientes);
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
                return NotFound();

            return Ok(cliente);
        }

        // POST: api/Clientes
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync(default);
            return CreatedAtAction(nameof(GetById), new { id = cliente.CodigoCliente }, cliente);
        }

        // PUT: api/Clientes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Cliente cliente)
        {
            if (id != cliente.CodigoCliente)
                return BadRequest();

            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync(default);
            return NoContent();
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
                return NotFound();

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync(default);
            return NoContent();
        }
    }
}