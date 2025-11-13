using Microsoft.EntityFrameworkCore;
using Moq;
using PostOfficeBackendProject.src.Application.Service;
using PostOfficeBackendProject.src.Domain.Interface;
using PostOfficeBackendProject.src.Domain.Model;
using PostOfficeBackendProject.src.Infrastructure.Data;
using PostOfficeBackendProject.src.Infrastructure.Repository;
using Xunit;

namespace PostOfficeProject.Tests.UnitTests
{
    public class DatabaseSeederServiceUnitTest : IAsyncLifetime
    {
        private ApplicationDBContext _dbContext;
        private DatabaseSeederService _service;
        private ITransportStatusRepository _repository;

        public async Task DisposeAsync()
        {
            await _dbContext.DisposeAsync();
        }

        public async Task InitializeAsync()
        {
            var databaseName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;
            _dbContext = new ApplicationDBContext(options);
            await _dbContext.Database.EnsureCreatedAsync();

            _repository = new TransportStatusRepository(_dbContext);

            _service = new DatabaseSeederService(_repository);

        }

        [Fact]
        public async Task DatabaseSeederService_SuccessfullSeed_ShouldReturnDefaultValuesForTransportstatus() 
        {
            //Arrange
            var expectedFirstRowResults = new TransportStatus
            {
                Status = "In Station"
            };

            var expectedSecondRowResults = new TransportStatus
            {
                Status = "Postman Delivering"
            };

            var expectedThirdRowResults = new TransportStatus
            {
                Status = "Postman Delivered"
            };

            //Act
            await _service.SeedAsync();

            var result = await _repository.GetAll();

            //Assert
            Assert.NotEqual(result.Count,0);
            Assert.Equal(result[0].Status, expectedFirstRowResults.Status);
            Assert.Equal(result[1].Status, expectedSecondRowResults.Status);
            Assert.Equal(result[2].Status, expectedThirdRowResults.Status);
        }
    }
}
