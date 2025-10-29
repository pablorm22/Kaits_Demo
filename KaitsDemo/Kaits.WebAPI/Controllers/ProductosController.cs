using Microsoft.AspNetCore.Mvc;
using Kaits.Application.Interfaces;
using Kaits.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kaits.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly IKaitsDbContext _context;

        public ProductosController(IKaitsDbContext context)
        {
            _context = context;
        }

        // GET: api/Productos
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var productos = await _context.Productos.ToListAsync();
            return Ok(productos);
        }

        // GET: api/Productos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
                return NotFound();

            return Ok(producto);
        }

        // POST: api/Productos
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Producto producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync(default);
            return CreatedAtAction(nameof(GetById), new { id = producto.CodigoProducto }, producto);
        }

        // PUT: api/Productos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Producto producto)
        {
            if (id != producto.CodigoProducto)
                return BadRequest();

            _context.Productos.Update(producto);
            await _context.SaveChangesAsync(default);
            return NoContent();
        }

        // DELETE: api/Productos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
                return NotFound();

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync(default);
            return NoContent();
        }
    }
}
