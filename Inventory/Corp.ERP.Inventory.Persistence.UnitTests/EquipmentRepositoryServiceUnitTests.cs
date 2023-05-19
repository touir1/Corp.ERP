using Corp.ERP.Common.Core;
using Corp.ERP.Common.Persistence.UnitTests;
using Corp.ERP.Common.Persistence.UnitTests.EFCore.Utils;
using Corp.ERP.Inventory.Domain.Models;
using Corp.ERP.Inventory.Infrastructure.Configurations;
using Corp.ERP.Inventory.Persistence;
using Corp.ERP.Inventory.Persistence.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Query;
using NSubstitute;

namespace Corp.ERP.Inventory.Persistance.UnitTests;

public class EquipmentRepositoryServiceUnitTests
{
    private static readonly string TEST_CONNECTION_STRING = "Data Source=../Databases/TestDB.db";

    [Fact]
    public async void ShouldReturnCorrectCountEquipmentsWhenGetAllAsync()
    {
        // Arrange
        IList<Equipment> equipments = PrepareMockData();
        var count = equipments.Count();
        var equipmentRepo = PrepareMock(equipments);

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
        var equipmentRepo = PrepareMock(equipments);

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
        var equipmentRepo = PrepareMock(equipments);
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
        var equipmentRepo = PrepareMock(equipments);
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
        var equipmentRepo = PrepareMock();

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
        var equipmentRepo = PrepareMock( equipments);
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
        var equipmentRepo = PrepareMock(equipments);
        var testCode = "";
        while (equipments.Where(w => w.Code == testCode).Count() > 0)
            testCode += StringUtils.GetRandomChar();
        var defaultResult = new Equipment();

        // Act
        Equipment result = await equipmentRepo.GetFirstOrDefaultAsync(a => a.Code == testCode, defaultResult);

        // Assert
        result.Should().NotBeNull();
        result.Should().Be(defaultResult);
    }

    [Fact]
    public async void ShouldUpdateEquipmentWhenUpdateAsync()
    {
        /*
         * To change later as it uses a real sqlite database for now
         * it throws errors related to Entry method calls if using NSubstitute
         * and problem with table creation if using in-memory
         */
        // Arrange
        var equipments = PrepareMockData();
        var equipmentRepo = PrepareMock(equipments, true);
        var id = equipments[0].Id.ToString();
        var newEquipment = new Equipment
        {
            Id = Guid.Parse(id),
            Name = "changed name",
            Code = equipments[0].Code,
            Description = equipments[0].Description,
            IsInUse = equipments[0].IsInUse,
            StartDateUsage = equipments[0].StartDateUsage,
            StorageUnit = equipments[0].StorageUnit,
            StorageUnitId = equipments[0].StorageUnitId,
            UsedBy = equipments[0].UsedBy,
            UsedById = equipments[0].UsedById,
        };

        // Act
        int nbResult = await equipmentRepo.UpdateAsync(newEquipment);
        var result = (await equipmentRepo.GetAllAsync()).Where(w => w.Id.ToString() == id).FirstOrDefault();

        // Assert
        nbResult.Should().Be(1);
        result.Should().NotBeNull();
        result.Name.Should().Be(newEquipment.Name);
        
    }

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
                ConnectionString = TEST_CONNECTION_STRING,
                EnsureCreated = true,
                EnsureDeleted = true,
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

    private EquipmentRepositoryService PrepareMock()
    {
        IList<Equipment> equipments = PrepareMockData();

        return PrepareMock(equipments);
    }

    private EquipmentRepositoryService PrepareMock(IList<Equipment> equipments, bool createTestDatabase = false)
    {
        var mockContext = PrepareContext(equipments, createTestDatabase);

        var equipmentRepo = new EquipmentRepositoryService(mockContext);

        return equipmentRepo;
    }
}