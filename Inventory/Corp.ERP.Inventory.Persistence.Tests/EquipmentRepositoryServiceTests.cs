using Corp.ERP.Common.Core;
using Corp.ERP.Common.Persistence.Tests;
using Corp.ERP.Inventory.Domain.Models;
using Corp.ERP.Inventory.Infrastructure.Configurations;
using Corp.ERP.Inventory.Persistence;
using Corp.ERP.Inventory.Persistence.Repositories;

namespace Corp.ERP.Inventory.Persistance.Tests;

public class EquipmentRepositoryServiceTests
{
    [Fact]
    public async void ShouldReturnCorrectCountEquipmentsWhenGetAllAsync()
    {
        // Arrange
        IList<Equipment> equipments = PrepareMockData();
        var count = equipments.Count();
        var equipmentRepo = PrepareMockRepo(equipments, true);

        // Act
        IList<Equipment> result = await equipmentRepo.GetAllAsync();

        // Assert
        result.Should().HaveCount(count);
    }

    [Fact]
    public async void ShouldReturnOneEquipmentWhenGetAllAsyncWithPredicateFixedCode()
    {
        // Arrange
        var count = 1;
        IList<Equipment> equipments = PrepareMockData();
        var testCode = equipments[0].Code;
        var equipmentRepo = PrepareMockRepo(equipments, true);

        // Act
        IList<Equipment> result = await equipmentRepo.GetAllAsync(a => a.Code == testCode);

        // Assert
        result.Should().NotBeNull().And.HaveCount(count);
        result[0].Code.Should().Be(testCode);
    }

    [Fact]
    public async void ShouldReturnEmptyEquipmentListWhenGetAllAsyncWithPredicateGotNoResult()
    {
        // Arrange
        var count = 0;
        IList<Equipment> equipments = PrepareMockData();
        var equipmentRepo = PrepareMockRepo(equipments, true);
        var testCode = "";
        while (equipments.Where(w => w.Code == testCode).Count() > 0)
            testCode += StringUtils.GetRandomChar();

        // Act
        IList<Equipment> result = await equipmentRepo.GetAllAsync(a => a.Code == testCode);

        // Assert
        result.Should().NotBeNull().And.HaveCount(count);
    }

    [Fact]
    public async void ShouldReturnEquipmentWhenGetByIdAsync()
    {
        // Arrange
        var equipments = PrepareMockData();
        var equipmentRepo = PrepareMockRepo(equipments, true);
        var testId = equipments[0].Id;

        // Act
        Equipment result = await equipmentRepo.GetByIdAsync(testId);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(testId);
    }

    [Fact]
    public async void ShouldReturnNullWhenGetByIdAsyncNotFound()
    {
        // Arrange
        var testId = Guid.NewGuid();
        var equipments = PrepareMockData();
        while (equipments.Where(w => w.Id.ToString().Equals(testId.ToString())).Count() > 0)
            testId = Guid.NewGuid();
        var equipmentRepo = PrepareMockRepo(equipments, true);

        // Act
        Equipment result = await equipmentRepo.GetByIdAsync(testId);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async void ShouldReturnEquipmentWhenGetFirstOrDefault()
    {
        // Arrange
        var equipments = PrepareMockData();
        var equipmentRepo = PrepareMockRepo(equipments, true);
        var isInUse = equipments[0].IsInUse;

        // Act
        Equipment result = await equipmentRepo.GetFirstOrDefaultAsync(e => e.IsInUse == isInUse, null);

        // Assert
        result.Should().NotBeNull();
        result.IsInUse.Should().Be(isInUse);
    }

    [Fact]
    public async void ShouldReturnDefaultWhenGetFirstOrDefaultNoResults()
    {
        // Arrange
        var equipments = PrepareMockData();
        var equipmentRepo = PrepareMockRepo(equipments, true);
        var testCode = "";
        while (equipments.Where(w => w.Code == testCode).Count() > 0)
            testCode += StringUtils.GetRandomChar();
        var defaultResult = new Equipment();

        // Act
        Equipment result = await equipmentRepo.GetFirstOrDefaultAsync(a => a.Code.Equals(testCode), defaultResult);

        // Assert
        result.Should().NotBeNull();
        result.Should().Be(defaultResult);
    }

    [Fact]
    public async void ShouldUpdateEquipmentWhenUpdateAsync()
    {
        // Arrange
        var equipments = PrepareMockData();
        var equipmentRepo = PrepareMockRepo(equipments, true);
        var id = equipments[0].Id.ToString();
        var newEquipment = new Equipment
        {
            Id = Guid.Parse(id),
            Name = "changed name",
            Code = equipments[0].Code,
            Description = equipments[0].Description,
            IsInUse = equipments[0].IsInUse,
            StartDateUsage = equipments[0].StartDateUsage,
            //StorageUnit = equipments[0].StorageUnit,
            StorageUnitId = equipments[0].StorageUnitId,
            //UsedBy = equipments[0].UsedBy,
            UsedById = equipments[0].UsedById,
        };

        // Act
        int nbResult = await equipmentRepo.UpdateAsync(newEquipment);
        //var result = await equipmentRepo.GetByIdAsync(Guid.Parse(id));

        // Assert
        nbResult.Should().Be(1);
        //result.Should().NotBeNull();
        //result.Name.Should().Be(newEquipment.Name);
        
    }

    [Fact]
    public async void ShouldAddEquipmentWhenAddAsync()
    {
        // Arrange
        var equipments = PrepareMockData();
        //int countBefore = equipments.Count();
        var equipmentRepo = PrepareMockRepo(equipments, true);
        var newEquipment = new Equipment
        {
            //Id = Guid.Parse(id),
            Name = "Test equipment",
            Code = "Test_code",
            Description = "this is a test equipment",
            IsInUse = false,
            //StartDateUsage = null,
            //StorageUnit = equipments[0].StorageUnit,
            StorageUnitId = equipments[0].StorageUnitId,
            //UsedBy = equipments[0].UsedBy,
            //UsedById = equipments[0].UsedById,
        };

        // Act
        int nbResult = await equipmentRepo.AddAsync(newEquipment);
        //var result = await equipmentRepo.GetAllAsync();

        // Assert
        nbResult.Should().Be(1);
        //result.Should().NotBeNull().And.HaveCount(countBefore + 1);
        newEquipment.Id.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public async void ShouldNotAddEquipmentWhenAddAsyncIdExists()
    {
        // Arrange
        var equipments = PrepareMockData();
        var equipmentRepo = PrepareMockRepo(equipments, true);
        var newEquipment = new Equipment
        {
            Id = equipments[0].Id,
            Name = "Test equipment",
            Code = "Test_code",
            Description = "this is a test equipment",
            IsInUse = false,
            StorageUnitId = equipments[0].StorageUnitId,
        };
        Exception exception = null;

        // Act
        int nbResult = 0;
        try
        {
            nbResult = await equipmentRepo.AddAsync(newEquipment);
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
        var equipments = PrepareMockData();
        var equipmentRepo = PrepareMockRepo(equipments, true);
        var toDeleteEquipment = new Equipment
        {
            Id = equipments[0].Id,
        };

        // Act
        int nbResult = await equipmentRepo.DeleteAsync(toDeleteEquipment);

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
                InMemoryDatabaseName = "TestDatabase",
            };
            var inventoryContext = new InventoryContext(configuration);

            inventoryContext.AddRange(list);
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