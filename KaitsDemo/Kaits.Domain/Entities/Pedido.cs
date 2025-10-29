using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kaits.Domain.Entities
{
    public class Pedido
    {
        [Key]
        public int IdOrden { get; set; }
        public DateTime FechaOrden { get; set; }
        public string CodigoCliente { get; set; } = string.Empty;
        public decimal PrecioTotal { get; set; }

        // Navegación
        public List<PedidoDetalle> Detalles { get; set; } = new();
    }
}