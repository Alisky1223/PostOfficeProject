using CommonDll.Dto;
using Moq;
using PostOfficeProject.Core.src.Domain.Interface;
using PostOfficeProject.Core.src.Domain.Model;
using PostOfficeProject.Core.src.Infrastructure.Midleware;
using System.Net;
using System.Text;
using System.Text.Json;
using Xunit;

namespace PostOfficeProject.Tests.UnitTests
{
    public class UsersMiddlewareUnitTest
    {

        private readonly Mock<ICustomerRepository> _mockCustomerRepo;
        private readonly Mock<IPostmanRepository> _mockPostmanRepo;
        private readonly UsersMiddleware _middleware;

        public UsersMiddlewareUnitTest()
        {
            _mockCustomerRepo = new Mock<ICustomerRepository>();
            _mockPostmanRepo = new Mock<IPostmanRepository>();
        }

        // Helper to create mocked HttpClient with custom response
        private static HttpClient CreateMockHttpClient(HttpResponseMessage responseMessage)
        {
            var handler = new MockHttpMessageHandler(responseMessage);
            var httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri("http://localhost:7157")
            };
            return httpClient;
        }

        // Custom handler to return predefined response
        private class MockHttpMessageHandler : HttpMessageHandler
        {
            private readonly HttpResponseMessage _response;

            public MockHttpMessageHandler(HttpResponseMessage response)
            {
                _response = response;
            }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                return Task.FromResult(_response);
            }
        }

        [Fact]
        public async Task GetAllUsers_Successful_ReturnsListOfUsers()
        {
            // Arrange
            var apiResponse = new ApiResponse<List<UserDto>>("Success", 200)
            {
                Data = [new UserDto { /* Fill props */ }, new UserDto { /* Fill props */ }]
            };
            var jsonResponse = JsonSerializer.Serialize(apiResponse);
            var mockHttpResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(jsonResponse, Encoding.UTF8, "application/json")
            };
            var httpClient = CreateMockHttpClient(mockHttpResponse);

            var middleware = new UsersMiddleware(httpClient, _mockCustomerRepo.Object, _mockPostmanRepo.Object);

            // Act
            var result = await middleware.GetAllUsers();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(2, result.Data.Count);
        }

        [Fact]
        public async Task GetUserInformation_CustomerRole_Successful_ReturnsUserCustomerDto()
        {
            // Arrange
            var userId = 1;
            var apiResponse = new ApiResponse<UserPersonalInformationDto>("Success", 200)
            {
                Data = new UserPersonalInformationDto { Role = new RoleDto { Name = "Customer" } /* Fill other props */ }
            };
            var jsonResponse = JsonSerializer.Serialize(apiResponse);
            var mockHttpResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(jsonResponse, Encoding.UTF8, "application/json")
            };
            var httpClient = CreateMockHttpClient(mockHttpResponse);

            var customer = new Customer { Id = 1, UserId = userId /* Fill props */ };
            _mockCustomerRepo.Setup(r => r.GetCustomerByUserIdAsync(userId)).ReturnsAsync(customer);

            var middleware = new UsersMiddleware(httpClient, _mockCustomerRepo.Object, _mockPostmanRepo.Object);

            // Act
            var result = await middleware.GetUserInformation(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode); // Assuming ApiResponse has StatusCode
            Assert.NotNull(result.Data);
            // Assert specific DTO properties, e.g., Assert.Equal("Customer", result.Data.Role.Name);
            _mockCustomerRepo.Verify(r => r.GetCustomerByUserIdAsync(userId), Times.Once);
            _mockPostmanRepo.Verify(r => r.GetPostmanByUserId(It.IsAny<int>()), Times.Never);
        }
    }
}
