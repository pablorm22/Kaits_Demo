using Microsoft.EntityFrameworkCore;
using Kaits.Domain.Entities;

namespace Kaits.Application.Interfaces
{
    public interface IKaitsDbContext
    {
        DbSet<Pedido> Pedido { get; }
        DbSet<Producto> Productos { get; }
        DbSet<Cliente> Clientes { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
