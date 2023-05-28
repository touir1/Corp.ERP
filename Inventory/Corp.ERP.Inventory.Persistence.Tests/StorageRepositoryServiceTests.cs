namespace Corp.ERP.Inventory.Persistence.Tests;

public class StorageRepositoryServiceTests
{
    [Fact]
    public async void ShouldReturnCorrectCountStoragesWhenGetAllAsync()
    {
        // Arrange
        IList<Storage> storages = PrepareMockData();
        var count = storages.Count();
        var repo = PrepareMockRepo(storages, true);

        // Act
        IList<Storage> result = await repo.GetAllAsync();

        // Assert
        result.Should().HaveCount(count);
    }

    #region Utils
    private InventoryContext PrepareContext(IList<Storage> list, bool createTestDatabase)
    {
        if (!createTestDatabase)
        {
            var mockConfiguration = Substitute.For<DbConfiguration>();
            var mockContext = Substitute.For<InventoryContext>(mockConfiguration);

            var mockSet = NSubstituteEFCoreUtils.GetDbSetSubstitute(list);
            mockContext.Set<Storage>().Returns(mockSet);
            mockContext.Storages = mockSet;

            return mockContext;
        }
        else
        {
            var configuration = new DbConfiguration
            {
                EnsureCreated = true,
                EnsureDeleted = true,
                UseInMemoryDatabase = true,
                InMemoryDatabaseName = "TestDatabase",
            };
            var inventoryContext = new InventoryContext(configuration);

            inventoryContext.Storages.AddRange(list);
            inventoryContext.SaveChanges();
            inventoryContext.ChangeTracker.Clear();

            return inventoryContext;
        }
    }

    private IList<Storage> PrepareMockData()
    {
        IList<Storage> list = new List<Storage>
        {
            new Storage {
                Id = Guid.NewGuid(),
                Name = "Storage 1",
                Address = "My company address"
            },
            new Storage
            {
                Id = Guid.NewGuid(),
                Name = "Storage 2",
                Address = "address 2"
            }
        };
        

        return list;
    }

    private StorageRepositoryService PrepareMockRepo(IList<Storage> equipments, bool createTestDatabase = false)
    {
        var mockContext = PrepareContext(equipments, createTestDatabase);

        var repo = new StorageRepositoryService(mockContext);

        return repo;
    }
    #endregion
}
