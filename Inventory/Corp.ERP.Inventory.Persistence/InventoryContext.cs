using Corp.ERP.Inventory.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Corp.ERP.Inventory.Infrastructure.Configurations;
using Microsoft.Data.Sqlite;
using Corp.ERP.Inventory.Domain.Models;

namespace Corp.ERP.Inventory.Persistence;

public class InventoryContext : DbContext
{
    private readonly DbConfiguration _configuration;

    public DbSet<Equipment> Equipments { get; set; } //{ get => Set<Equipment>(); }

    public InventoryContext(DbConfiguration configuration)
    {
        _configuration = configuration;
        if (_configuration is not null && _configuration.EnsureCreated)
        {
            // ensure folder created
            var builder = new SqliteConnectionStringBuilder(_configuration.ConnectionString);
            var dbPath = builder.DataSource;
            var directoryPath = Path.GetDirectoryName(dbPath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            // ensure sqlite database created
            Database.EnsureCreated();
        }
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
