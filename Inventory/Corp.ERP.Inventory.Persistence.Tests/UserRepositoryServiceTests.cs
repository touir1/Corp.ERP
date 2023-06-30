namespace Corp.ERP.Inventory.Persistence.Tests;

public class UserRepositoryServiceTests
{
    [Fact]
    public async void ShouldReturnCorrectCountUsersWhenGetAllAsync()
    {
        // Arrange
        IList<User> list = PrepareMockData();
        var count = list.Count();
        var repo = PrepareMockRepo(list, true);

        // Act
        IList<User> result = await repo.GetAllAsync();

        // Assert
        result.Should().HaveCount(count);
    }

    [Fact]
    public async void ShouldReturnOneUserWhenGetAllAsyncWithPredicateFixedId()
    {
        // Arrange
        IList<User> list = PrepareMockData();
        var count = 1;
        var id = list[0].Id;
        var repo = PrepareMockRepo(list, true);

        // Act
        IList<User> result = await repo.GetAllAsync(s => s.Id.ToString().Equals(id.ToString()));

        // Assert
        result.Should().HaveCount(count);
    }

    [Fact]
    public async void ShouldReturnEmptyUserListWhenGetAllAsyncWithPredicateGotNoResult()
    {
        // Arrange
        var count = 0;
        IList<User> list = PrepareMockData();
        var repo = PrepareMockRepo(list, true);
        var id = Guid.NewGuid().ToString();
        while (list.Where(w => w.Id.ToString().Equals(id)).Count() > 0)
            id = Guid.NewGuid().ToString();

        // Act
        IList<User> result = await repo.GetAllAsync(a => a.Id.ToString().Equals(id));

        // Assert
        result.Should().NotBeNull().And.HaveCount(count);
    }

    [Fact]
    public async void ShouldReturnUserWhenGetByIdAsync()
    {
        // Arrange
        var list = PrepareMockData();
        var repo = PrepareMockRepo(list, true);
        var id = list[0].Id;

        // Act
        User result = await repo.GetByIdAsync(id);

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
        User result = await repo.GetByIdAsync(id);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async void ShouldReturnUserWhenGetFirstOrDefault()
    {
        // Arrange
        var list = PrepareMockData();
        var repo = PrepareMockRepo(list, true);
        var firstname = list[0].FirstName;

        // Act
        User result = await repo.GetFirstOrDefaultAsync(e => e.FirstName == firstname, null);

        // Assert
        result.Should().NotBeNull();
        result.FirstName.Should().Be(firstname);
    }

    [Fact]
    public async void ShouldReturnDefaultWhenGetFirstOrDefaultNoResults()
    {
        // Arrange
        var list = PrepareMockData();
        var repo = PrepareMockRepo(list, true);
        var firstname = "";
        while (list.Where(w => w.FirstName == firstname).Count() > 0)
            firstname += StringUtils.GetRandomChar();
        var defaultResult = new User();

        // Act
        User result = await repo
            .GetFirstOrDefaultAsync(a => a.FirstName.Equals(firstname), defaultResult);

        // Assert
        result.Should().NotBeNull();
        result.Should().Be(defaultResult);
    }

    [Fact]
    public async void ShouldUpdateUserWhenUpdateAsync()
    {
        // Arrange
        var list = PrepareMockData();
        var repo = PrepareMockRepo(list, true);
        var id = list[0].Id.ToString();
        var newEntity = new User
        {
            Id = Guid.Parse(id),
            FirstName = "changed firstname"
        };

        // Act
        int nbResult = await repo.UpdateAsync(newEntity);

        // Assert
        nbResult.Should().Be(1);

    }

    [Fact]
    public async void ShouldAddUserWhenAddAsync()
    {
        // Arrange
        var list = PrepareMockData();
        var repo = PrepareMockRepo(list, true);
        var newEntity = new User
        {
            FirstName = "new firstname",
            LastName = "new lastname",
            Email = "new.mail@email.com"
        };

        // Act
        int nbResult = await repo.AddAsync(newEntity);

        // Assert
        nbResult.Should().Be(1);
        newEntity.Id.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public async void ShouldNotAddUserWhenAddAsyncIdExists()
    {
        // Arrange
        var list = PrepareMockData();
        var repo = PrepareMockRepo(list, true);
        var newEntity = new User
        {
            Id = list[0].Id,
            FirstName = "new firstname",
            LastName = "new lastname",
            Email = "new.user@email.com"
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
    public async void ShouldRemoveUserWhenDeleteAsync()
    {
        // Arrange
        var list = PrepareMockData();
        var repo = PrepareMockRepo(list, true);
        var toDeleteEntity = new User
        {
            Id = list[0].Id,
        };

        // Act
        int nbResult = await repo.DeleteAsync(toDeleteEntity);

        // Assert
        nbResult.Should().Be(1);
    }

    #region Utils
    private InventoryContext PrepareContext(IList<User> list, bool createTestDatabase)
    {
        if (!createTestDatabase)
        {
            var mockConfiguration = Substitute.For<InventoryDbConfiguration>();
            var mockContext = Substitute.For<InventoryContext>(mockConfiguration);

            var mockSet = NSubstituteEFCoreUtils.GetDbSetSubstitute(list);
            mockContext.Set<User>().Returns(mockSet);
            mockContext.Users = mockSet;

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

            inventoryContext.Users.AddRange(list);
            inventoryContext.SaveChanges();
            inventoryContext.ChangeTracker.Clear();

            return inventoryContext;
        }
    }

    private IList<User> PrepareMockData()
    {
        IList<User> list = new List<User>
        {
            new User {
                Id = Guid.NewGuid(),
                Email = "test1.mail@email.com",
                FirstName = "Test_f_1",
                LastName = "Test_l_1",
            },
            new User
            {
                Id = Guid.NewGuid(),
                Email = "test2.mail@email.com",
                FirstName = "Test_f_2",
                LastName = "Test_l_2",
            }
        };


        return list;
    }

    private UserRepositoryService PrepareMockRepo(IList<User> list, bool createTestDatabase = false)
    {
        var mockContext = PrepareContext(list, createTestDatabase);

        var repo = new UserRepositoryService(mockContext);

        return repo;
    }
    #endregion
}
