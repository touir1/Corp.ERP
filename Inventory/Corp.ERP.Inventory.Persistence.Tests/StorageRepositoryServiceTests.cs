namespace Corp.ERP.Inventory.Persistence.Tests;

public class StorageRepositoryServiceTests
{
    [Fact]
    public async void ShouldReturnCorrectCountStoragesWhenGetAllAsync()
    {
        // Arrange
        IList<Storage> list = PrepareMockData();
        var count = list.Count();
        var repo = PrepareMockRepo(list, true);

        // Act
        IList<Storage> result = await repo.GetAllAsync();

        // Assert
        result.Should().HaveCount(count);
    }

    [Fact]
    public async void ShouldReturnOneStorageWhenGetAllAsyncWithPredicateFixedId()
    {
        // Arrange
        IList<Storage> list = PrepareMockData();
        var count = 1;
        var id = list[0].Id;
        var repo = PrepareMockRepo(list, true);

        // Act
        IList<Storage> result = await repo.GetAllAsync(s => s.Id.ToString().Equals(id.ToString()));

        // Assert
        result.Should().HaveCount(count);
    }

    [Fact]
    public async void ShouldReturnEmptyStorageListWhenGetAllAsyncWithPredicateGotNoResult()
    {
        // Arrange
        var count = 0;
        IList<Storage> list = PrepareMockData();
        var repo = PrepareMockRepo(list, true);
        var id = Guid.NewGuid().ToString();
        while (list.Where(w => w.Id.ToString().Equals(id)).Count() > 0)
            id = Guid.NewGuid().ToString();

        // Act
        IList<Storage> result = await repo.GetAllAsync(a => a.Id.ToString().Equals(id));

        // Assert
        result.Should().NotBeNull().And.HaveCount(count);
    }

    [Fact]
    public async void ShouldReturnStorageWhenGetByIdAsync()
    {
        // Arrange
        var list = PrepareMockData();
        var repo = PrepareMockRepo(list, true);
        var id = list[0].Id;

        // Act
        Storage result = await repo.GetByIdAsync(id);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(id);
    }

    [Fact]
    public async void ShouldReturnNullWhenGetByIdAsyncNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        var list = PrepareMockData();
        while (list.Where(w => w.Id.ToString().Equals(id.ToString())).Count() > 0)
            id = Guid.NewGuid();
        var repo = PrepareMockRepo(list, true);

        // Act
        Storage result = await repo.GetByIdAsync(id);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async void ShouldReturnStorageWhenGetFirstOrDefault()
    {
        // Arrange
        var list = PrepareMockData();
        var repo = PrepareMockRepo(list, true);
        var name = list[0].Name;

        // Act
        Storage result = await repo.GetFirstOrDefaultAsync(e => e.Name == name, null);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(name);
    }

    [Fact]
    public async void ShouldReturnDefaultWhenGetFirstOrDefaultNoResults()
    {
        // Arrange
        var list = PrepareMockData();
        var repo = PrepareMockRepo(list, true);
        var name = "";
        while (list.Where(w => w.Name == name).Count() > 0)
            name += StringUtils.GetRandomChar();
        var defaultResult = new Storage();

        // Act
        Storage result = await repo.GetFirstOrDefaultAsync(a => a.Name.Equals(name), defaultResult);

        // Assert
        result.Should().NotBeNull();
        result.Should().Be(defaultResult);
    }

    [Fact]
    public async void ShouldUpdateStorageWhenUpdateAsync()
    {
        // Arrange
        var list = PrepareMockData();
        var repo = PrepareMockRepo(list, true);
        var id = list[0].Id.ToString();
        var newEntity = new Storage
        {
            Id = Guid.Parse(id),
            Name = "changed name"
        };

        // Act
        int nbResult = await repo.UpdateAsync(newEntity);

        // Assert
        nbResult.Should().Be(1);

    }

    [Fact]
    public async void ShouldAddStorageWhenAddAsync()
    {
        // Arrange
        var list = PrepareMockData();
        var repo = PrepareMockRepo(list, true);
        var newEntity = new Storage
        {
            Name = "Test equipment",
            Address = "Test address"
        };

        // Act
        int nbResult = await repo.AddAsync(newEntity);

        // Assert
        nbResult.Should().Be(1);
        newEntity.Id.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public async void ShouldNotAddStorageWhenAddAsyncIdExists()
    {
        // Arrange
        var list = PrepareMockData();
        var repo = PrepareMockRepo(list, true);
        var newEntity = new Storage
        {
            Id = list[0].Id,
            Name = "Test equipment",
            Address = "Test address"
        };
        Exception? exception = null;

        // Act
        int nbResult = 0;
        try
        {
            nbResult = await repo.AddAsync(newEntity);
        }
        catch (Exception ex)
        {
            exception = ex;
        }

        // Assert
        nbResult.Should().Be(0);
        exception.Should().NotBeNull().And.BeOfType<ArgumentException>();
    }

    [Fact]
    public async void ShouldRemoveStorageWhenDeleteAsync()
    {
        // Arrange
        var list = PrepareMockData();
        var repo = PrepareMockRepo(list, true);
        var toDeleteEntity = new Storage
        {
            Id = list[0].Id,
        };

        // Act
        int nbResult = await repo.DeleteAsync(toDeleteEntity);

        // Assert
        nbResult.Should().Be(1);
    }

    #region Utils
    private InventoryContext PrepareContext(IList<Storage> list, bool createTestDatabase)
    {
        if (!createTestDatabase)
        {
            var mockConfiguration = Substitute.For<InventoryDbConfiguration>();
            var mockContext = Substitute.For<InventoryContext>(mockConfiguration);

            var mockSet = NSubstituteEFCoreUtils.GetDbSetSubstitute(list);
            mockContext.Set<Storage>().Returns(mockSet);
            mockContext.Storages = mockSet;

            return mockContext;
        }
        else
        {
            var configuration = new InventoryDbConfiguration
            {
                EnsureCreated = true,
                EnsureDeleted = true,
                UseInMemoryDatabase = true,
                InMemoryDatabaseName = StringUtils.GetRandomString(20),
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
