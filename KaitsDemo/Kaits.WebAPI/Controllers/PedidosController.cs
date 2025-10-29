using Kaits.Domain.Entities;
using Kaits.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kaits.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly KaitsDbContext _context;

        public PedidosController(KaitsDbContext context)
        {
            _context = context;
        }

        // GET: api/pedidos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidos()
        {
            var pedidos = await _context.Pedido
                .Include(p => p.Detalles)
                .ToListAsync();

            return Ok(pedidos);
        }

        // GET: api/pedidos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetPedido(int id)
        {
            var pedido = await _context.Pedido
                .Include(p => p.Detalles)
                .FirstOrDefaultAsync(p => p.IdOrden == id);

            if (pedido == null)
                return NotFound();

            return Ok(pedido);
        }

         // POST: api/pedidos
        [HttpPost]
        public async Task<ActionResult<Pedido>> PostPedido([FromBody] Pedido pedido)
        {
            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(pedido));
            
            if (pedido.Detalles == null || !pedido.Detalles.Any())
                return BadRequest("El pedido debe tener al menos un detalle.");

            // 🧮 Cálculo automático de subtotales y total
            foreach (var detalle in pedido.Detalles)
                detalle.Subtotal = detalle.Cantidad * detalle.PrecioUnitario;

            pedido.PrecioTotal = pedido.Detalles.Sum(d => d.Subtotal);

            _context.Pedido.Add(pedido);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPedido), new { id = pedido.IdOrden }, pedido);
        }

        // PUT: api/pedidos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPedido(int id, [FromBody] Pedido pedido)
        {
            if (id != pedido.IdOrden)
                return BadRequest("El ID no coincide con el pedido.");

            var pedidoExistente = await _context.Pedido
                .Include(p => p.Detalles)
                .FirstOrDefaultAsync(p => p.IdOrden == id);

            if (pedidoExistente == null)
                return NotFound();

            // Actualizar campos principales
            pedidoExistente.FechaOrden = pedido.FechaOrden;
            pedidoExistente.CodigoCliente = pedido.CodigoCliente;

            // 🔁 Actualizar detalles
            _context.DetallePedido.RemoveRange(pedidoExistente.Detalles);
            pedidoExistente.Detalles = pedido.Detalles;

            // 🧮 Recalcular subtotales y total
            foreach (var d in pedidoExistente.Detalles)
                d.Subtotal = d.Cantidad * d.PrecioUnitario;

            pedidoExistente.PrecioTotal = pedidoExistente.Detalles.Sum(d => d.Subtotal);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/pedidos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(int id)
        {
            var pedido = await _context.Pedido
                .Include(p => p.Detalles)
                .FirstOrDefaultAsync(p => p.IdOrden == id);

            if (pedido == null)
                return NotFound();

            _context.Pedido.Remove(pedido);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
