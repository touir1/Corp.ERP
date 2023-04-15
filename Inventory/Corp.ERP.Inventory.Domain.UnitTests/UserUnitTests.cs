namespace Corp.ERP.Inventory.Domain.UnitTests;

public class UserUnitTests
{
    [Fact]
    public void ShouldReturnFullNameWhenFirstNameAndLastNameSet()
    {
        // Arrange
        string expectedFullName = "John Doe";
        var person = Substitute.For<User>();
        person.FirstName.Returns("John");
        person.LastName.Returns("Doe");

        // Act
        string actualFullName = person.FullName;

        // Assert
        actualFullName.Should().Be(expectedFullName);
    }
}