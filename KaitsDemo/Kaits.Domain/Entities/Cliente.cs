using System.ComponentModel.DataAnnotations;

namespace Kaits.Domain.Entities
{
    public class Cliente
    {
        [Key]
        public int CodigoCliente { get; set; }
        public string NombreApellido { get; set; } = string.Empty;
        public string DNI { get; set; } = string.Empty;
    }
}
