using Corp.ERP.Inventory.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Corp.ERP.Inventory.Infrastructure.Configurations;
using Microsoft.Data.Sqlite;
using Corp.ERP.Inventory.Domain.Models;
using Microsoft.Extensions.Logging;

namespace Corp.ERP.Inventory.Persistence;

public class InventoryContext : DbContext
{
    private readonly DbConfiguration _configuration;

    public DbSet<Equipment> Equipments { get; set; }
    public DbSet<Storage> Storages { get; set; }
    public DbSet<User> Users { get; set; }

    public InventoryContext(DbConfiguration configuration)
    {
        _configuration = configuration;
        if (_configuration is not null && (_configuration.EnsureCreated | _configuration.EnsureDeleted))
        {
            // ensure folder created
            var builder = new SqliteConnectionStringBuilder(_configuration.ConnectionString);
            var dbPath = builder.DataSource;
            if(dbPath != null) 
            {
                var directoryPath = Path.GetDirectoryName(dbPath);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
            }

            if (_configuration.EnsureDeleted)
                Database.EnsureDeleted();
            if(_configuration.EnsureCreated)
                Database.EnsureCreated();
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = _configuration.ConnectionString;

        optionsBuilder.UseSqlite(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Information);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(EquipmentConfiguration).Assembly);

    }
}
