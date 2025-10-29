using Microsoft.EntityFrameworkCore;
using Kaits.Application.Interfaces;
using Kaits.Domain.Entities;

namespace Kaits.Infrastructure.Persistence
{
    public class KaitsDbContext : DbContext, IKaitsDbContext
    {
        public KaitsDbContext(DbContextOptions<KaitsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Pedido> Pedido => Set<Pedido>();
        public DbSet<PedidoDetalle> DetallePedido => Set<PedidoDetalle>();
        public DbSet<Producto> Productos => Set<Producto>();
        public DbSet<Cliente> Clientes => Set<Cliente>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relación Pedido → Detalle
            modelBuilder.Entity<Pedido>()
                .HasMany(p => p.Detalles)
                .WithOne()
                .HasForeignKey(d => d.IdOrden)
                .OnDelete(DeleteBehavior.Cascade);

            // Configurar precisión de campos monetarios
            modelBuilder.Entity<Pedido>()
                .Property(p => p.PrecioTotal)
                .HasPrecision(10, 2);

            modelBuilder.Entity<PedidoDetalle>()
                .Property(d => d.PrecioUnitario)
                .HasPrecision(10, 2);

            modelBuilder.Entity<PedidoDetalle>()
                .Property(d => d.Subtotal)
                .HasPrecision(10, 2);
        }

        /*public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }*/
    }
}