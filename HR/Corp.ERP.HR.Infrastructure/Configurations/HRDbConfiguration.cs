namespace Corp.ERP.HR.Infrastructure.Configurations;

public class HRDbConfiguration
{
    public bool EnsureCreated { get; set; }
    public bool EnsureDeleted { get; set; }
    public string ConnectionString { get; set; }
    public bool UseInMemoryDatabase { get; set; }
    public string InMemoryDatabaseName { get; set; }
}