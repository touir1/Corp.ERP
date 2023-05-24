using Corp.ERP.Inventory.Application.Contract.Dto;
using Corp.ERP.Inventory.Application.Queries.GetEquipments;
using Corp.ERP.Inventory.Service.RestAPI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Corp.ERP.Inventory.Service.RestAPI.Tests;

public class EquipmentsControllerTests
{
    [Fact]
    public async void ShouldReturnTwoEquipmentsWhenGetEquipments()
    {
        // Arrange
        var count = 2;

        var mediator = Substitute.For<IMediator>();
        StorageDto storage = new StorageDto
        {
            Id = Guid.NewGuid(),
            Name = "Storage 1",
            Address = "My company address"
        };
        UserDto user = new UserDto
        {
            Id = Guid.NewGuid(),
            Email = "user1@email.com",
            FirstName = "Test",
            LastName = "User",
        };
        var equipments = new List<EquipmentDto>()
        {
            new EquipmentDto
            {
                Id = Guid.NewGuid(),
                Code = "TEST_1",
                Description = "Description_1",
                IsInUse = false,
                Name = "Test 1",
                StorageUnit = storage
            },
                new EquipmentDto
            {
                Id = Guid.NewGuid() ,
                Code = "Test_2",
                Description = "Description_2",
                IsInUse = true,
                Name = "Test 2",
                StorageUnit = storage,
                StartDateUsage = new DateTime(2023,01,01,8,0,0),
                UsedBy = user
            }
        };
        
        var expectedResult = new GetEquipmentsQueryResult
        {
            Equipments = equipments,
        };
        mediator.Send(Arg.Any<GetEquipmentsQuery>()).Returns(expectedResult);

        var controller = new EquipmentsController(mediator);

        // Act
        var result = await controller.GetEquipments(new GetEquipmentsQuery());

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        var okResult = result.As<OkObjectResult>();
        okResult.Value.Should().BeOfType<GetEquipmentsQueryResult>();
        var queryResult = okResult.Value.As<GetEquipmentsQueryResult>();
        queryResult.Equipments.Should().HaveCount(count);
    }
}