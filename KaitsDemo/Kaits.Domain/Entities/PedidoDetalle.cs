using System.ComponentModel.DataAnnotations;

namespace Kaits.Domain.Entities
{
    public class PedidoDetalle
    {
        [Key]
        public int IdDetalle { get; set; }   // Primary Key
        public int IdOrden { get; set; }  // Foreign Key
        public string CodigoProducto { get; set; } = string.Empty;
        public string DescripcionProducto { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }
}
