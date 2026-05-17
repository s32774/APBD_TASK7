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
        modelBuilder.Entity<ComponentManufacturer>().HasData(
    new ComponentManufacturer
    {
        Id = 1,
        Abbreviation = "INT",
        FullName = "Intel",
        FoundationDate = new DateTime(1968, 7, 18)
    },
    new ComponentManufacturer
    {
        Id = 2,
        Abbreviation = "AMD",
        FullName = "Advanced Micro Devices",
        FoundationDate = new DateTime(1969, 5, 1)
    },
    new ComponentManufacturer
    {
        Id = 3,
        Abbreviation = "NV",
        FullName = "Nvidia",
        FoundationDate = new DateTime(1993, 4, 5)
    }
);

modelBuilder.Entity<ComponentType>().HasData(
    new ComponentType { Id = 1, Abbreviation = "CPU", Name = "Processor" },
    new ComponentType { Id = 2, Abbreviation = "GPU", Name = "Graphics Card" },
    new ComponentType { Id = 3, Abbreviation = "RAM", Name = "Memory" }
);

modelBuilder.Entity<Component>().HasData(
    new Component
    {
        Code = "CPU001",
        Name = "Intel Core i5",
        Description = "Processor for office computers",
        ComponentManufacturerId = 1,
        ComponentTypeId = 1
    },
    new Component
    {
        Code = "CPU002",
        Name = "AMD Ryzen 5",
        Description = "Processor for gaming computers",
        ComponentManufacturerId = 2,
        ComponentTypeId = 1
    },
    new Component
    {
        Code = "GPU001",
        Name = "Nvidia RTX 4060",
        Description = "Graphics card for gaming",
        ComponentManufacturerId = 3,
        ComponentTypeId = 2
    }
);

modelBuilder.Entity<Pc>().HasData(
    new Pc
    {
        Id = 1,
        Name = "Gaming Beast X",
        Weight = 12.5m,
        Warranty = 36,
        CreatedAt = new DateTime(2026, 5, 8, 9, 0, 0),
        Stock = 5
    },
    new Pc
    {
        Id = 2,
        Name = "Office Mini Pro",
        Weight = 4.2m,
        Warranty = 24,
        CreatedAt = new DateTime(2026, 4, 15, 13, 30, 0),
        Stock = 12
    },
    new Pc
    {
        Id = 3,
        Name = "Student PC",
        Weight = 6.5m,
        Warranty = 12,
        CreatedAt = new DateTime(2026, 3, 10, 10, 0, 0),
        Stock = 8
    }
);

modelBuilder.Entity<PcComponent>().HasData(
    new PcComponent { PcId = 1, ComponentCode = "CPU002", Amount = 1 },
    new PcComponent { PcId = 1, ComponentCode = "GPU001", Amount = 1 },
    new PcComponent { PcId = 2, ComponentCode = "CPU001", Amount = 1 }
);
    }
}