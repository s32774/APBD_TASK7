namespace APBD_TASK7.Data;
using APBD_TASK7.model;
using Microsoft.EntityFrameworkCore;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Pc> Pcs { get; set; }
    public DbSet<Component> Components { get; set; }
    public DbSet<ComponentType> ComponentTypes { get; set; }
    public DbSet<ComponentManufacturer> ComponentManufacturers { get; set; }
    public DbSet<PcComponent> PcComponents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PcComponent>()
            .HasKey(pc => new { pc.PcId, pc.ComponentCode });

        modelBuilder.Entity<PcComponent>()
            .HasOne(pc => pc.Pc)
            .WithMany(p => p.PcComponents)
            .HasForeignKey(pc => pc.PcId);

        modelBuilder.Entity<PcComponent>()
            .HasOne(pc => pc.Component)
            .WithMany(c => c.PcComponents)
            .HasForeignKey(pc => pc.ComponentCode);

        modelBuilder.Entity<Component>()
            .HasKey(c => c.Code);

        modelBuilder.Entity<Component>()
            .HasOne(c => c.ComponentManufacturer)
            .WithMany(m => m.Components)
            .HasForeignKey(c => c.ComponentManufacturerId);

        modelBuilder.Entity<Component>()
            .HasOne(c => c.ComponentType)
            .WithMany(t => t.Components)
            .HasForeignKey(c => c.ComponentTypeId);
    }
}