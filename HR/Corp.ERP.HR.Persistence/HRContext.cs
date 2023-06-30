using Corp.ERP.HR.Domain.Models;
using Corp.ERP.HR.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Corp.ERP.HR.Persistence;

public class HRContext : DbContext
{
    private readonly HRDbConfiguration _configuration;

    public DbSet<User> Users { get; set; }
    public DbSet<Title> Titles { get; set; }
    public DbSet<Identification> Identifications { get; set; }
    public DbSet<IdentificationType> IdentificationTypes { get; set; }

    public HRContext(HRDbConfiguration configuration)
    {
        _configuration = configuration;
        if (_configuration is not null && (_configuration.EnsureCreated | _configuration.EnsureDeleted))
        {
            if (_configuration.EnsureDeleted)
                Database.EnsureDeleted();
            if (_configuration.EnsureCreated)
                Database.EnsureCreated();
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (_configuration.UseInMemoryDatabase)
        {
            optionsBuilder
                .UseInMemoryDatabase(databaseName: _configuration.InMemoryDatabaseName)
                .LogTo(Console.WriteLine, LogLevel.Information)
#if DEBUG
                .EnableSensitiveDataLogging()
#endif
                ;
        }
        else
        {
            var connectionString = _configuration.ConnectionString;

            optionsBuilder
                .UseSqlServer(connectionString)
                .LogTo(Console.WriteLine, LogLevel.Information)
#if DEBUG
                .EnableSensitiveDataLogging()
#endif
                ;
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    }
}