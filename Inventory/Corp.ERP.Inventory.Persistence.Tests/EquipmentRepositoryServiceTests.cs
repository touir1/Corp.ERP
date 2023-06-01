using Corp.ERP.Common.Core;

namespace Corp.ERP.Inventory.Persistance.Tests;

public class EquipmentRepositoryServiceTests
{
    [Fact]
    public async void ShouldReturnCorrectCountEquipmentsWhenGetAllAsync()
    {
        // Arrange
        IList<Equipment> list = PrepareMockData();
        var count = list.Count();
        var repo = PrepareMockRepo(list, true);

        // Act
        IList<Equipment> result = await repo.GetAllAsync();

        // Assert
        result.Should().HaveCount(count);
    }

    [Fact]
    public async void ShouldReturnOneEquipmentWhenGetAllAsyncWithPredicateFixedCode()
    {
        // Arrange
        var count = 1;
        IList<Equipment> list = PrepareMockData();
        var code = list[0].Code;
        var repo = PrepareMockRepo(list, true);

        // Act
        IList<Equipment> result = await repo.GetAllAsync(a => a.Code == code);

        // Assert
        result.Should().NotBeNull().And.HaveCount(count);
        result[0].Code.Should().Be(code);
    }

    [Fact]
    public async void ShouldReturnEmptyEquipmentListWhenGetAllAsyncWithPredicateGotNoResult()
    {
        // Arrange
        var count = 0;
        IList<Equipment> list = PrepareMockData();
        var repo = PrepareMockRepo(list, true);
        var code = "";
        while (list.Where(w => w.Code == code).Count() > 0)
            code += StringUtils.GetRandomChar();

        // Act
        IList<Equipment> result = await repo.GetAllAsync(a => a.Code == code);

        // Assert
        result.Should().NotBeNull().And.HaveCount(count);
    }

    [Fact]
    public async void ShouldReturnEquipmentWhenGetByIdAsync()
    {
        // Arrange
        var list = PrepareMockData();
        var repo = PrepareMockRepo(list, true);
        var id = list[0].Id;

        // Act
        Equipment result = await repo.GetByIdAsync(id);

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
        Equipment result = await repo.GetByIdAsync(id);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async void ShouldReturnEquipmentWhenGetFirstOrDefault()
    {
        // Arrange
        var list = PrepareMockData();
        var repo = PrepareMockRepo(list, true);
        var isInUse = list[0].IsInUse;

        // Act
        Equipment result = await repo.GetFirstOrDefaultAsync(e => e.IsInUse == isInUse, null);

        // Assert
        result.Should().NotBeNull();
        result.IsInUse.Should().Be(isInUse);
    }

    [Fact]
    public async void ShouldReturnDefaultWhenGetFirstOrDefaultNoResults()
    {
        // Arrange
        var list = PrepareMockData();
        var repo = PrepareMockRepo(list, true);
        var code = "";
        while (list.Where(w => w.Code == code).Count() > 0)
            code += StringUtils.GetRandomChar();
        var defaultResult = new Equipment();

        // Act
        Equipment result = await repo.GetFirstOrDefaultAsync(a => a.Code.Equals(code), defaultResult);

        // Assert
        result.Should().NotBeNull();
        result.Should().Be(defaultResult);
    }

    [Fact]
    public async void ShouldUpdateEquipmentWhenUpdateAsync()
    {
        // Arrange
        var list = PrepareMockData();
        var repo = PrepareMockRepo(list, true);
        var id = list[0].Id.ToString();
        var newEquipment = new Equipment
        {
            Id = Guid.Parse(id),
            Name = "changed name",
            Code = list[0].Code,
            Description = list[0].Description,
            IsInUse = list[0].IsInUse,
            StartDateUsage = list[0].StartDateUsage,
            StorageUnitId = list[0].StorageUnitId,
            UsedById = list[0].UsedById,
        };

        // Act
        int nbResult = await repo.UpdateAsync(newEquipment);

        // Assert
        nbResult.Should().Be(1);
        
    }

    [Fact]
    public async void ShouldAddEquipmentWhenAddAsync()
    {
        // Arrange
        var list = PrepareMockData();
        var repo = PrepareMockRepo(list, true);
        var newEquipment = new Equipment
        {
            //Id = Guid.Parse(id),
            Name = "Test equipment",
            Code = "Test_code",
            Description = "this is a test equipment",
            IsInUse = false,
            StorageUnitId = list[0].StorageUnitId,
        };

        // Act
        int nbResult = await repo.AddAsync(newEquipment);

        // Assert
        nbResult.Should().Be(1);
        newEquipment.Id.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public async void ShouldNotAddEquipmentWhenAddAsyncIdExists()
    {
        // Arrange
        var list = PrepareMockData();
        var repo = PrepareMockRepo(list, true);
        var newEquipment = new Equipment
        {
            Id = list[0].Id,
            Name = "Test equipment",
            Code = "Test_code",
            Description = "this is a test equipment",
            IsInUse = false,
            StorageUnitId = list[0].StorageUnitId,
        };
        Exception? exception = null;

        // Act
        int nbResult = 0;
        try
        {
            nbResult = await repo.AddAsync(newEquipment);
        }
        catch(Exception ex)
        {
            exception = ex;
        }
        
        // Assert
        nbResult.Should().Be(0);
        exception.Should().NotBeNull().And.BeOfType<ArgumentException>();
    }

    [Fact]
    public async void ShouldRemoveEquipmentWhenDeleteAsync()
    {
        // Arrange
        var list = PrepareMockData();
        var repo = PrepareMockRepo(list, true);
        var toDeleteEquipment = new Equipment
        {
            Id = list[0].Id,
        };

        // Act
        int nbResult = await repo.DeleteAsync(toDeleteEquipment);

        // Assert
        nbResult.Should().Be(1);
    }

    #region Utils
    private InventoryContext PrepareContext(IList<Equipment> list, bool createTestDatabase)
    {
        if (!createTestDatabase)
        {
            var mockConfiguration = Substitute.For<DbConfiguration>();
            var mockContext = Substitute.For<InventoryContext>(mockConfiguration);

            var mockSetEquipments = NSubstituteEFCoreUtils.GetDbSetSubstitute(list);
            mockContext.Set<Equipment>().Returns(mockSetEquipments);
            mockContext.Equipments = mockSetEquipments;

            return mockContext;
        }
        else
        {
            var configuration = new DbConfiguration
            {
                EnsureCreated = true,
                EnsureDeleted = true,
                UseInMemoryDatabase = true,
                InMemoryDatabaseName = StringUtils.GetRandomString(20),
            };
            var inventoryContext = new InventoryContext(configuration);

            inventoryContext.Equipments.AddRange(list);
            inventoryContext.SaveChanges();
            inventoryContext.ChangeTracker.Clear();

            return inventoryContext;
        }
    }

    private IList<Equipment> PrepareMockData()
    {
        Storage storage = new Storage
        {
            Id = Guid.NewGuid(),
            Name = "Storage 1",
            Address = "My company address"
        };
        User user = new User
        {
            Id = Guid.NewGuid(),
            Email = "user1@email.com",
            FirstName = "Test",
            LastName = "User",
        };

        IList<Equipment> equipments = new List<Equipment>()
        {
            new Equipment
            {
                Id = Guid.NewGuid(),
                Code = "TEST_1",
                Description = "Description_1",
                IsInUse = false,
                Name = "Test 1",
                StorageUnit = storage
            },
                new Equipment
            {
                Id = Guid.NewGuid() ,
                Code = "Test_2",
                Description = "Description_2",
                IsInUse = true,
                Name = "Test 2",
                StorageUnit = storage,
                StartDateUsage = new DateTime(2023,01,01,8,0,0),
                UsedBy = user,
                UsedById = user.Id
            }
        };

        return equipments;
    }

    private EquipmentRepositoryService PrepareMockRepo()
    {
        IList<Equipment> equipments = PrepareMockData();

        return PrepareMockRepo(equipments);
    }

    private EquipmentRepositoryService PrepareMockRepo(IList<Equipment> equipments, bool createTestDatabase = false)
    {
        var mockContext = PrepareContext(equipments, createTestDatabase);

        var equipmentRepo = new EquipmentRepositoryService(mockContext);

        return equipmentRepo;
    }
    #endregion
}