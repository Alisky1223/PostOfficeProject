using CommonDll.Dto;
using Microsoft.EntityFrameworkCore;
using PostOfficeBackendProject.src.Domain.Model;
using PostOfficeBackendProject.src.Infrastructure.Data;
using PostOfficeBackendProject.src.Infrastructure.Repository;
using Xunit;

namespace PostOfficeProject.Tests.UnitTests
{
    public class PostmanRepositoryUnitTest : IAsyncLifetime
    {
        private ApplicationDBContext _dbContext;
        private PostmanRepository _repository;

        public async Task InitializeAsync()
        {
            var databaseName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;
            _dbContext = new ApplicationDBContext(options);
            await _dbContext.Database.EnsureCreatedAsync();
            _repository = new PostmanRepository(_dbContext);
        }

        public async Task DisposeAsync()
        {
            await _dbContext.DisposeAsync();
        }

        [Fact]
        public async Task GetAllPostmenAsync_ReturnsListOfPostmen()
        {
            // Arrange
            var newpostman = new Postman
            {
                Id = 1,
                PostOfficeId = 1,
                UserId = 1,
                PersonalCode = "EMP123"
            };

            await _dbContext.Postman.AddAsync(newpostman);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _repository.GetPostmanById(1);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Postman>(result);
            Assert.Equal(newpostman.Id, result.Id);
            Assert.Equal(newpostman.PostOfficeId, result.PostOfficeId);
            Assert.Equal(newpostman.UserId, result.UserId);
            Assert.Equal(newpostman.PersonalCode, result.PersonalCode);
        }

    }
}
