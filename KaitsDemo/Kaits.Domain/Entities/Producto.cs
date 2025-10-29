using System.ComponentModel.DataAnnotations;

namespace Kaits.Domain.Entities
{
    public class Producto
    {
        [Key]
        public int CodigoProducto { get; set; }
        public string Descripcion { get; set; } = string.Empty;
    }
}