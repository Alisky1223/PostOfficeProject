using Microsoft.EntityFrameworkCore;
using PostOfficeProject.Core.src.Domain.Model;
using PostOfficeProject.Core.src.Infrastructure.Data;
using PostOfficeProject.Core.src.Infrastructure.Repository;
using Xunit;

namespace PostOfficeProject.Tests.UnitTests
{
    public class CustomerRepositoryUnitTest : IAsyncLifetime
    {

        private ApplicationDBContext? _dbContext;
        private CustomerRepository? _repository;

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
            _repository = new CustomerRepository(_dbContext);
        }

        [Fact]
        public async Task CreateNewCustomer_ShouldAddAndRetrieveCorrectly()
        {

            //Arrange
            if (_repository == null) throw new InvalidOperationException("repository is null");

            var newCustomer = new Customer
            {
                CustomerNumber = "Test Customer Number",
                Id = 1,
                UserId = 1,
                Products = []
            };

            //Act

            await _repository.CreateAsync(newCustomer);
            var firstRowResult = await _repository.GetCustomerByIdAsync(1);
            //Assert

            Assert.NotNull(firstRowResult);
            Assert.Equal(firstRowResult.CustomerNumber, newCustomer.CustomerNumber);
            Assert.Equal(firstRowResult.Id, newCustomer.Id);
            Assert.Equal(firstRowResult.UserId, newCustomer.UserId);
        }

        [Fact]
        public async Task UpdateCustomer_ShouldModifyExistingAndRetrieveUpdated()
        {
            //Arrange
            if (_repository == null) throw new InvalidOperationException("repository is null");

            var newCustomer = new Customer
            {
                CustomerNumber = "Test Customer Number",
                Id = 1,
                UserId = 1,
                Products = []
            };
            await _repository.CreateAsync(newCustomer);

            var updatedCustomer = new Customer
            {
                CustomerNumber = "Test Updated Customer Number",
                Id = 1,
                UserId = 2,
                Products = []
            };

            //Act

            var updateResult = await _repository.UpdateCustomerAsync(1, updatedCustomer);
            var retrievedCustomer = await _repository.GetCustomerByIdAsync(1);

            // Assert
            Assert.NotNull(updateResult);
            Assert.NotNull(retrievedCustomer);
            Assert.Equal(updatedCustomer.CustomerNumber, retrievedCustomer.CustomerNumber);
            Assert.Equal(updatedCustomer.UserId, retrievedCustomer.UserId);
            Assert.Equal(1, retrievedCustomer.Id); // Id unchanged
        }

        [Fact]
        public async Task UpdateCustomerAsync_NonExistentId_ShouldReturnNull()
        {

            //Arrange
            if (_repository == null) throw new InvalidOperationException("repository is null");

            var nonExistedCustomer = new Customer { Id = 999, UserId = 1, CustomerNumber = "NonExistent" };

            // Act
            var result = await _repository.UpdateCustomerAsync(999, nonExistedCustomer);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllCustomersAsync_WithMultipleCustomers_ShouldReturnAll()
        {
            // Arrange
            if (_repository == null) throw new InvalidOperationException("repository is null");

            await _repository.CreateAsync(new Customer { Id = 1, UserId = 1, CustomerNumber = "One" });
            await _repository.CreateAsync(new Customer { Id = 2, UserId = 2, CustomerNumber = "Two" });

            // Act
            var customers = await _repository.GetAllCustomersAsync();

            // Assert
            Assert.Equal(2, customers.Count);
        }

        [Fact]
        public async Task GetCustomerByUserIdAsync_WithIncludes_ShouldLoadRelatedEntities()
        {
            // Arrange
            if (_repository == null) throw new InvalidOperationException("repository is null");
            if (_dbContext == null) throw new InvalidOperationException("db context is null");

            var productType = new ProductType { Id = 1, Type = "Soft" };

            var transportStatus = new TransportStatus { Id = 1, Status = "Dliverd" };

            _dbContext.ProductType.Add(productType);
            _dbContext.TransportStatus.Add(transportStatus);

            await _dbContext.SaveChangesAsync();

            var customer = new Customer
            {
                Id = 1,
                UserId = 1,
                CustomerNumber = "WithProducts",
                Products = [new Product { ProductName = "Test Product", CustomerId = 1, Description = "Test Description", PostmanId = null, PostOfficeId = null, ProductTypeId = 1, Id = 1, Price = 489.50m, TransportStatusId = 1 }]
            };
            await _repository.CreateAsync(customer);

            // Act
            var result = await _repository.GetCustomerByUserIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result.Products);

        }

    }
}
