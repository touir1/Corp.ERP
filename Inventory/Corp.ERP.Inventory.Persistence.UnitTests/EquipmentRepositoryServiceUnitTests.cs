using Corp.ERP.Common.Persistence.UnitTests;
using Corp.ERP.Inventory.Domain.Models;
using Corp.ERP.Inventory.Infrastructure.Configurations;
using Corp.ERP.Inventory.Persistence;
using Corp.ERP.Inventory.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using NSubstitute;

namespace Corp.ERP.Inventory.Persistance.UnitTests;

public class EquipmentRepositoryServiceUnitTests
{
    
    [Fact]
    public async void ShouldReturnTwoEquipmentsWhenGetAllAsync()
    {
        // Arrange
        var count = 2;
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

        var mockContext = PrepareContext(equipments);
        var equipmentRepo = new EquipmentRepositoryService(mockContext);

        // Act
        IList<Equipment> result = await equipmentRepo.GetAllAsync();

        // Assert
        result.Should().HaveCount(count);
    }

    public InventoryContext PrepareContext(IList<Equipment> list)
    {
        var mockConfiguration = Substitute.For<DbConfiguration>();
        var mockContext = Substitute.For<InventoryContext>(mockConfiguration);

        var mockSetEquipments = NSubstituteEFCoreUtils.GetDbSetSubstitute(list);
        mockContext.Set<Equipment>().Returns(mockSetEquipments);
        mockContext.Equipments = mockSetEquipments;

        return mockContext;
    }
}