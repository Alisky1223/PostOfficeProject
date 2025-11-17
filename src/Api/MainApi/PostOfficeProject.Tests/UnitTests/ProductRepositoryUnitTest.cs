using Microsoft.EntityFrameworkCore;
using PostOfficeProject.Core.src.Domain.Model;
using PostOfficeProject.Core.src.Infrastructure.Data;
using PostOfficeProject.Core.src.Infrastructure.Repository;
using Xunit;
using Xunit.Sdk;

namespace PostOfficeProject.Tests.UnitTests
{
    public class ProductRepositoryUnitTest : IAsyncLifetime
    {
        private ApplicationDBContext? _dbContext;
        private ProductRepository? _repository;

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
            _repository = new ProductRepository(_dbContext);
        }

        [Fact]
        public async Task GetAllProduct_Success_ShouldReturnAllProducts()
        {
            //Arrange
            if (_dbContext == null) throw new InvalidOperationException("db context is null");
            if (_repository == null) throw new InvalidOperationException("repository is null");

            var firstProduct = new Product
            {
                Price = 100,
                Description = "Test Description #1",
                ProductName = "Test Product Name #1",
            };

            var secondProduct = new Product
            {
                Price = 200,
                Description = "Test Description #2",
                ProductName = "Test Product Name #2",
            };

            await _dbContext.Product.AddAsync(firstProduct);
            await _dbContext.Product.AddAsync(secondProduct);

            await _dbContext.SaveChangesAsync();

            //Act
            var result = await _repository.GetAllAsync();

            //Assert
            Assert.NotEmpty(result);
            CheckResultsAreEqual(firstProduct, result[0]);
            CheckResultsAreEqual(secondProduct, result[1]);
        }

        [Fact]
        public async Task GetByIdProduct_Success_ShouldReturnProduct()
        {
            //Arrange
            if (_dbContext == null) throw new InvalidOperationException("db context is null");
            if (_repository == null) throw new InvalidOperationException("repository is null");

            var firstProduct = new Product
            {
                Price = 100,
                Description = "Test Description #1",
                ProductName = "Test Product Name #1",
            };

            await _dbContext.Product.AddAsync(firstProduct);

            await _dbContext.SaveChangesAsync();

            //Act
            var result = await _repository.GetById(1);

            //Assert
            Assert.NotNull(result);
            CheckResultsAreEqual(firstProduct, result);
        }

        [Fact]
        public async Task GetByIdProduct_Failed_ShouldReturnNull()
        {
            //Arrange
            if (_repository == null) throw new InvalidOperationException("repository is null");

            //Act
            var result = await _repository.GetById(1);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateProduct_Success_ShouldReturnNewProduct()
        {
            //Arrange
            if (_dbContext == null) throw new InvalidOperationException("db context is null");
            if (_repository == null) throw new InvalidOperationException("repository is null");

            var notExpectedProduct = new Product
            {
                Price = 100,
                Description = "Test Description #1",
                ProductName = "Test Product Name #1",
            };

            var firstProduct = new Product
            {
                Price = 100,
                Description = "Test Description #1",
                ProductName = "Test Product Name #1",
            };

            var secondProduct = new Product
            {
                Price = 200,
                Description = "Test Description #2",
                ProductName = "Test Product Name #2",
            };

            await _dbContext.Product.AddAsync(firstProduct);

            await _dbContext.SaveChangesAsync();

            //Act
            var result = await _repository.UpdateAsync(1, secondProduct);

            //Assert
            Assert.NotNull(result);
            CheckResultsAreEqual(secondProduct, result);
            CheckResultsAreNotEqual(notExpectedProduct, result);
        }

        [Fact]
        public async Task UpdateProduct_Failed_ShouldReturnNull()
        {
            //Arrange
            if (_repository == null) throw new InvalidOperationException("repository is null");

            //Act
            var result = await _repository.UpdateAsync(1, new Product { });

            //Assert
            Assert.Null(result);
        }

        private static void CheckResultsAreEqual(Product expected, Product result)
        {
            Assert.Equal(expected.Price, result.Price);
            Assert.Equal(expected.Description, result.Description);
            Assert.Equal(expected.ProductName, result.ProductName);
        }

        private static void CheckResultsAreNotEqual(Product expected, Product result)
        {
            Assert.NotEqual(expected.Price, result.Price);
            Assert.NotEqual(expected.Description, result.Description);
            Assert.NotEqual(expected.ProductName, result.ProductName);
        }
    }
}
