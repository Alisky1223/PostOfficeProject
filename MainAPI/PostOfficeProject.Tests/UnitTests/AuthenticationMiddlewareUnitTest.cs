using CommonDll.Dto;
using PostOfficeBackendProject.src.Infrastructure.Midleware;
using System.Net;
using System.Text;
using System.Text.Json;
using Xunit;

namespace PostOfficeProject.Tests.UnitTests
{
    public class AuthenticationMiddlewareUnitTest
    {

        private readonly AuthenticationMiddleware _middleware;

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
        public async Task Login_SuccessfulResponse_ReturnsApiResponseWithData()
        {
            //Arrange
            var expectedResponse = new ApiResponse<string>("Login success") { Data = "auth-token" };
            var jsonResponse = JsonSerializer.Serialize(expectedResponse);
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(jsonResponse, Encoding.UTF8, "application/json")
            };
            var httpClient = CreateMockHttpClient(mockResponse);
            var middleware = new AuthenticationMiddleware(httpClient);
            var loginDto = new LoginDto { Username = "Alireza", Password = "P@ssword_44" };

            //Act
            var result = await middleware.Login(loginDto);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal("auth-token", result.Data);
        }

        [Fact]
        public async Task Login_FailedResponse_ReturnsErrorApiResponse()
        {
            // Arrange
            var expectedResponse = new ApiResponse<string>("Invalid Response", 500);
            var jsonResponse = JsonSerializer.Serialize(expectedResponse);
            var mockResponse = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(jsonResponse, Encoding.UTF8, "application/json")
            };
            var httpClient = CreateMockHttpClient(mockResponse);
            var middleware = new AuthenticationMiddleware(httpClient);
            var loginDto = new LoginDto { Username = "Alireza", Password = "PWsssrods" };

            // Act 
            var result = await middleware.Login(loginDto);
            //Assert
            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
            Assert.Equal("Invalid Response", result.Message);
        }

        [Fact]
        public async Task Register_SuccessfulResponse_ReturnsApiResponseWithData()
        {
            // Arrange
            var expectedResponse = new ApiResponse<string>("Registration successful") { Data = "Done" };
            var jsonResponse = JsonSerializer.Serialize(expectedResponse);
            var mockResponse = new HttpResponseMessage(HttpStatusCode.Created)
            {
                Content = new StringContent(jsonResponse, Encoding.UTF8, "application/json")
            };
            var httpClient = CreateMockHttpClient(mockResponse);
            var middleware = new AuthenticationMiddleware(httpClient);
            var registerDto = new RegisterDto { FirstName = "Alireza", Password = "P@ass", ConfirmPassword = "Pass", LastName = "Asadi", UserEmail = "ali.r.asadi75@gmail.com", Username = "AliSJ", UserPhone = "+45975267555" };

            // Act
            var result = await middleware.Register(registerDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal("Done", result.Data);
        }


    }
}
