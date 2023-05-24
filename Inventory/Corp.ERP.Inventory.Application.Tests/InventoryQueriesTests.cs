using Corp.ERP.Inventory.Application.Contract.Repositories;
using Corp.ERP.Inventory.Application.Queries.GetEquipments;
using Corp.ERP.Inventory.Domain.Models;
using FluentAssertions.Execution;

namespace Corp.ERP.Inventory.Application.Tests
{
    public class InventoryQueriesTests
    {
        [Fact]
        public async Task ShouldReturnTwoEquipmentsWhenGetEquipmentsAsync()
        {
            // Arrange
            var equipments = PrepareMockData();
            var count = equipments.Count();

            IEquipmentRepositoryService service = Substitute.For<IEquipmentRepositoryService>();

            service.GetAllAsync().Returns(Task.FromResult(equipments));

            GetEquipmentsQuery query = Substitute.For<GetEquipmentsQuery>();
            GetEquipmentsQueryHandler handler = Substitute.For<GetEquipmentsQueryHandler>(service);

            // Act
            var result = handler.Handle(query, CancellationToken.None);

            // Assert
            using (new AssertionScope())
            {
                result.IsCompletedSuccessfully.Should().BeTrue();
                result.Result.Should().NotBeNull();
                result.Result.Equipments.Should().HaveCount(count);
            }
            
        }

        public IList<Equipment> PrepareMockData()
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
    }
}