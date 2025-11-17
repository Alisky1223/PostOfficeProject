using CommonDll.Dto;
using Microsoft.EntityFrameworkCore;
using PostOfficeProject.Core.src.Domain.Model;
using PostOfficeProject.Core.src.Infrastructure.Data;
using PostOfficeProject.Core.src.Infrastructure.Repository;
using Xunit;

namespace PostOfficeProject.Tests.UnitTests
{
    public class PostmanRepositoryUnitTest : IAsyncLifetime
    {
        private ApplicationDBContext? _dbContext;
        private PostmanRepository? _repository;

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
            if (_dbContext == null) throw new InvalidOperationException("db context is null");

            await _dbContext.DisposeAsync();
        }

        [Fact]
        public async Task GetPostmanByIdAsync_ReturnsPostmen()
        {
            // Arrange
            if (_dbContext == null) throw new InvalidOperationException("db context is null");
            if (_repository == null) throw new InvalidOperationException("repository is null");

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
            ComparePostmansEqual(result, newpostman);
        }

        [Fact]
        public async Task GetPostmanByIdAsync_ReturnsNull()
        {
            // Arrange
            if (_dbContext == null) throw new InvalidOperationException("db context is null");
            if (_repository == null) throw new InvalidOperationException("repository is null");

            // Act
            var result = await _repository.GetPostmanById(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetPostmanByUserIdAsync_ReturnsPostmen()
        {
            // Arrange
            if (_dbContext == null) throw new InvalidOperationException("db context is null");
            if (_repository == null) throw new InvalidOperationException("repository is null");

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
            var result = await _repository.GetPostmanByUserId(1);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Postman>(result);
            ComparePostmansEqual(result, newpostman);
        }


        [Fact]
        public async Task GetPostmanByUserIdAsync_ReturnsNull()
        {
            // Arrange
            if (_repository == null) throw new InvalidOperationException("repository is null");

            // Act
            var result = await _repository.GetPostmanByUserId(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllPostmen()
        {
            // Arrange
            if (_dbContext == null) throw new InvalidOperationException("db context is null");
            if (_repository == null) throw new InvalidOperationException("repository is null");

            var firstPostman = new Postman
            {
                Id = 1,
                PostOfficeId = 1,
                UserId = 1,
                PersonalCode = "EMP123"
            };

            await _dbContext.Postman.AddAsync(firstPostman);

            var secondPostman = new Postman
            {
                Id = 2,
                PostOfficeId = 2,
                UserId = 2,
                PersonalCode = "EMP456"
            };

            await _dbContext.Postman.AddAsync(secondPostman);

            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _repository.GetPostmen();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Postman>>(result);
            ComparePostmansEqual(firstPostman, result[0]);
            ComparePostmansEqual(secondPostman, result[1]);
        }

        [Fact]
        public async Task PostmanRepository_CreatePostman_ShouldCreateNewPostman()
        {
            // Arrange
            if (_repository == null) throw new InvalidOperationException("repository is null");

            var newpostman = new Postman
            {
                Id = 1,
                PostOfficeId = 1,
                UserId = 1,
                PersonalCode = "EMP123"
            };

            // Act
            var result = await _repository.Create(newpostman);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Postman>(result);
            ComparePostmansEqual(result, newpostman);
        }

        [Fact]
        public async Task PostmanRepository_CreatePostman_ShouldReturnAllreadyPostman()
        {
            // Arrange
            if (_repository == null) throw new InvalidOperationException("repository is null");

            var postman = new Postman
            {
                Id = 1,
                PostOfficeId = 1,
                UserId = 1,
                PersonalCode = "EMP123"
            };
            await _repository.Create(postman);

            var newpostman = new Postman
            {
                Id = 2,
                PostOfficeId = 2,
                UserId = 1,
                PersonalCode = "EMP456"
            };
            // Act
            var result = await _repository.Create(newpostman);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Postman>(result);
            ComparePostmansEqual(result, postman);
            ComparePostmansNotEqual(result, newpostman);
        }

        [Fact]
        public async Task PostmanRepository_UpdatePostman_ShouldReturnUpdatedPostman()
        {
            // Arrange
            if (_repository == null) throw new InvalidOperationException("repository is null");

            var notExpected = new Postman
            {
                Id = 1,
                PostOfficeId = 1,
                UserId = 1,
                PersonalCode = "EMP123"
            };

            var postman = new Postman
            {
                Id = 1,
                PostOfficeId = 1,
                UserId = 1,
                PersonalCode = "EMP123"
            };
            await _repository.Create(postman);

            var newpostman = new Postman
            {
                Id = 2,
                PostOfficeId = 2,
                UserId = 1,
                PersonalCode = "EMP456"
            };
            // Act
            var result = await _repository.Update(1, newpostman);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Postman>(result);
            ComparePostmansEqual(result, newpostman);
            ComparePostmansNotEqual(result, notExpected);
        }

        [Fact]
        public async Task PostmanRepository_UpdatePostman_ShouldReturnNull()
        {
            // Arrange
            if (_repository == null) throw new InvalidOperationException("repository is null");

            var newpostman = new Postman
            {
                Id = 2,
                PostOfficeId = 2,
                UserId = 1,
                PersonalCode = "EMP456"
            };

            // Act
            var result = await _repository.Update(3, newpostman);

            // Assert
            Assert.Null(result);
        }

        private static void ComparePostmansEqual(Postman result, Postman expected)
        {
            Assert.Equal(expected.PostOfficeId, result.PostOfficeId);
            Assert.Equal(expected.UserId, result.UserId);
            Assert.Equal(expected.PersonalCode, result.PersonalCode);
        }

        private static void ComparePostmansNotEqual(Postman result, Postman expected)
        {

            Assert.NotEqual(expected.PostOfficeId, result.PostOfficeId);
            Assert.NotEqual(expected.PersonalCode, result.PersonalCode);
        }
    }
}
