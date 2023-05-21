namespace Corp.ERP.Inventory.Infrastructure.Configurations;

public class DbConfiguration
{
    public Boolean EnsureCreated { get; set; }
    public Boolean EnsureDeleted { get; set; }
    public String ConnectionString { get; set; }
    public Boolean UseInMemoryDatabase { get; set; }
    public String InMemoryDatabaseName { get; set; }
}
