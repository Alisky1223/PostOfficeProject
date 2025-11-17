using Microsoft.EntityFrameworkCore;
using Moq;
using PostOfficeProject.Core.src.Application.Service;
using PostOfficeProject.Core.src.Domain.Interface;
using PostOfficeProject.Core.src.Domain.Model;
using PostOfficeProject.Core.src.Infrastructure.Data;
using PostOfficeProject.Core.src.Infrastructure.Repository;
using Xunit;

namespace PostOfficeProject.Tests.UnitTests
{
    public class DatabaseSeederServiceUnitTest : IAsyncLifetime
    {
        private ApplicationDBContext? _dbContext;
        private DatabaseSeederService? _service;
        private ITransportStatusRepository? _repository;

        public async Task DisposeAsync()
        {
            if (_dbContext == null) throw new InvalidOperationException("db context is null");

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
            if (_service == null) throw new InvalidOperationException("db context is null");
            if (_repository == null) throw new InvalidOperationException("db context is null");

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
            Assert.NotEmpty(result);
            Assert.Equal(result[0].Status, expectedFirstRowResults.Status);
            Assert.Equal(result[1].Status, expectedSecondRowResults.Status);
            Assert.Equal(result[2].Status, expectedThirdRowResults.Status);
        }
    }
}
