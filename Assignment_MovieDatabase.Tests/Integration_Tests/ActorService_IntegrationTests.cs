
using Microsoft.EntityFrameworkCore;
using Assignment_MovieDatabase.Console.Contexts;
using Assignment_MovieDatabase.Console.Repositories;
using Assignment_MovieDatabase.Console.Services;

namespace Assignment_MovieDatabase.Tests.Unit_Tests
{
    public class ActorService_UnitTests
    {
        [Fact]
        public async Task RemoveAsync_Should_ReturnFalse_WhenIdDoesNotExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                .Options;

            using (var context = new DataContext(options))
            {
                var actorRepository = new ActorRepository(context);
                var actorService = new ActorService(actorRepository);

                // Act
                var result = await actorService.RemoveAsync(-1);

                // Assert
                Assert.False(result);
            }
        }

    }
}