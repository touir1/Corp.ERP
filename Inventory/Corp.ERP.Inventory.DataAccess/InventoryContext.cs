using Corp.ERP.Inventory.Persistence.Configurations;
using Corp.ERP.Inventory.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Corp.ERP.Inventory.Persistence;

public class InventoryContext : DbContext
{
    private readonly IConfiguration _configuration;

    public DbSet<Equipment> Equipments { get; set; }

    public InventoryContext(IConfiguration configuration)
    {
        _configuration = configuration;
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = _configuration.GetConnectionString("Default");

        optionsBuilder.UseSqlite(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(EquipmentConfiguration).Assembly);
    }
}
