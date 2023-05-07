using Corp.ERP.Common.Persistence.UnitTests;
using Corp.ERP.Inventory.Domain.Models;
using Corp.ERP.Inventory.Infrastructure.Configurations;
using Corp.ERP.Inventory.Persistence;
using Corp.ERP.Inventory.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using NSubstitute.Extensions;

namespace Corp.ERP.Inventory.Persistance.UnitTests;

public class EquipmentRepositoryServiceUnitTests
{
    [Fact]
    public void ShouldReturnTwoEquipmentsWhenGetAll()
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

        var mockConfiguration = Substitute.For<DbConfiguration>();
        var mockContext = Substitute.For<InventoryContext>(mockConfiguration);
        //mockContext.Database.Returns(Substitute.For<DatabaseFacade>(mockContext));

        var mockSet = NSubstituteEFCoreUtils.GetDbSetSubstitute(equipments);
        mockContext.Set<Equipment>().Returns(mockSet);
        //mockContext.Equipments.Returns(mockContext.Set<Equipment>());
        //mockContext.Equipments.Include("Equipment.StorageUnit").Returns(mockSet);
        //mockContext.Equipments.Include("Equipment.UsedBy").Returns(mockSet);

        var equipmentRepo = new EquipmentRepositoryService(mockContext);

        // Act
        IList<Equipment> result = equipmentRepo.GetAll();

        // Assert
        result.Should().HaveCount(count);
        
    }
}