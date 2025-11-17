using Microsoft.EntityFrameworkCore;
using PostOfficeProject.Core.src.Domain.Model;
using PostOfficeProject.Core.src.Infrastructure.Data;
using PostOfficeProject.Core.src.Infrastructure.Repository;
using Xunit;

namespace PostOfficeProject.Tests.UnitTests
{
    public class PostofficeRepositoryUnitTest : IAsyncLifetime
    {
        private ApplicationDBContext? _dbContext;
        private PostOfficeRepository? _repository;

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
            _repository = new PostOfficeRepository(_dbContext);
        }

        [Fact]
        public async Task GetAllPostoffice_Success_ShouldReturnAllPostoffices() 
        {
            //Arrange
            if (_dbContext == null) throw new InvalidOperationException("db context is null");
            if (_repository == null) throw new InvalidOperationException("repository is null");

            var firstPostOffice = new PostOffice
            {
                Id = 1,
                Address = "Test Address #1",
                OfficeAccessCode = "Test Access Code #1",
                OfficeName = "Test Office Name #1",
                StorageCapacity = 100,
            };

            var secondPostOffice = new PostOffice
            {
                Id = 2,
                Address = "Test Address #2",
                OfficeAccessCode = "Test Access Code #2",
                OfficeName = "Test Office Name #2",
                StorageCapacity = 200,
            };


            await _dbContext.PostOffice.AddAsync(firstPostOffice);
            await _dbContext.PostOffice.AddAsync(secondPostOffice);

            await _dbContext.SaveChangesAsync();

            //Act
            var result = await _repository.GetAllPostsAsync();

            //Assert
            Assert.NotEmpty(result);
            CheckResultsAreEqual(firstPostOffice, result[0]);
            CheckResultsAreEqual(secondPostOffice, result[1]);
        }

        [Fact]
        public async Task GetByIdPostoffice_Success_ShouldReturnPostoffice()
        {
            //Arrange
            if (_dbContext == null) throw new InvalidOperationException("db context is null");
            if (_repository == null) throw new InvalidOperationException("repository is null");

            var firstPostOffice = new PostOffice
            {
                Id = 1,
                Address = "Test Address #1",
                OfficeAccessCode = "Test Access Code #1",
                OfficeName = "Test Office Name #1",
                StorageCapacity = 100,
            };

            await _dbContext.PostOffice.AddAsync(firstPostOffice);

            await _dbContext.SaveChangesAsync();

            //Act
            var result = await _repository.GetPostByIdAsync(1);

            //Assert
            Assert.NotNull(result);
            CheckResultsAreEqual(firstPostOffice, result);
        }

        [Fact]
        public async Task GetByIdPostoffice_Failed_ShouldReturnNull()
        {
            //Arrange
            if (_repository == null) throw new InvalidOperationException("repository is null");

            //Act
            var result = await _repository.GetPostByIdAsync(1);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdatePostoffice_Success_ShouldReturnUpdatedPostoffice()
        {
            //Arrange
            if (_dbContext == null) throw new InvalidOperationException("db context is null");
            if (_repository == null) throw new InvalidOperationException("repository is null");

            var notExpectedResult = new PostOffice
            {
                Id = 1,
                Address = "Test Address #1",
                OfficeAccessCode = "Test Access Code #1",
                OfficeName = "Test Office Name #1",
                StorageCapacity = 100,
            };

            var firstPostOffice = new PostOffice
            {
                Id = 1,
                Address = "Test Address #1",
                OfficeAccessCode = "Test Access Code #1",
                OfficeName = "Test Office Name #1",
                StorageCapacity = 100,
            };

            var secondPostOffice = new PostOffice
            {
                Id = 2,
                Address = "Test Address #2",
                OfficeAccessCode = "Test Access Code #2",
                OfficeName = "Test Office Name #2",
                StorageCapacity = 200,
            };

            await _dbContext.PostOffice.AddAsync(firstPostOffice);

            await _dbContext.SaveChangesAsync();

            //Act
            var result = await _repository.UpdatePostAsync(1,secondPostOffice);

            //Assert
            Assert.NotNull(result);
            CheckResultsNotEqual(result, notExpectedResult);
            CheckResultsAreEqual(secondPostOffice, result);
        }

        [Fact]
        public async Task UpdatePostoffice_Failed_ShouldReturnNull()
        {
            //Arrange
            if (_repository == null) throw new InvalidOperationException("repository is null");

            //Act
            var result = await _repository.UpdatePostAsync(1, new PostOffice { });

            //Assert
            Assert.Null(result);
        }

        private static void CheckResultsAreEqual(PostOffice expected, PostOffice result) 
        {
            Assert.Equal(expected.OfficeName, result.OfficeName);
            Assert.Equal(expected.OfficeAccessCode, result.OfficeAccessCode);
            Assert.Equal(expected.Address, result.Address);           
            Assert.Equal(expected.StorageCapacity, result.StorageCapacity);
        }

        private static void CheckResultsNotEqual(PostOffice expected, PostOffice result)
        {
            Assert.NotEqual(expected.OfficeName, result.OfficeName);
            Assert.NotEqual(expected.OfficeAccessCode, result.OfficeAccessCode);
            Assert.NotEqual(expected.Address, result.Address);
            Assert.NotEqual(expected.StorageCapacity, result.StorageCapacity);
        }
    }
}
