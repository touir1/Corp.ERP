using Corp.ERP.Inventory.Persistence.Configurations;
using Corp.ERP.Inventory.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Corp.ERP.Inventory.Infrastructure.Configurations;

namespace Corp.ERP.Inventory.Persistence;

public class InventoryContext : DbContext
{
    private readonly DbConfiguration _configuration;

    public DbSet<Equipment> Equipments { get; set; } //{ get => Set<Equipment>(); }

    public InventoryContext(DbConfiguration configuration)
    {
        _configuration = configuration;
        //Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = _configuration.ConnectionString;

        optionsBuilder.UseSqlite(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(EquipmentConfiguration).Assembly);

    }
}
