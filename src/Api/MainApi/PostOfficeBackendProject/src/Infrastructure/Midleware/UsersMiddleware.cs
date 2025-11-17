using CommonDll.Dto;
using PostOfficeProject.Core.src.Application.Mapper;
using PostOfficeProject.Core.src.Domain.Interface;
using System.Text.Json;

namespace PostOfficeProject.Core.src.Infrastructure.Midleware
{
    public class UsersMiddleware : IUsersMiddleware
    {
        private readonly HttpClient _context;
        private readonly ICustomerRepository _customerRepository;
        private readonly IPostmanRepository _postmanRepository;
        public UsersMiddleware(HttpClient context, ICustomerRepository customerRepository, IPostmanRepository postmanRepository)
        {
            _context = context;
            _customerRepository = customerRepository;
            _postmanRepository = postmanRepository;
        }

        public async Task<ApiResponse<UserCustomerPostmanDto>> GetUserInformation(int userId)
        {
            try
            {
                var result = await _context.GetFromJsonAsync<ApiResponse<UserPersonalInformationDto>>($"/api/users/getUserById/{userId}") ?? throw new Exception("API response is null");

                ArgumentNullException.ThrowIfNull(result.Data);
                ArgumentNullException.ThrowIfNull(result.Data.Role);

                switch (result.Data.Role.Name)
                {
                    case "Customer":
                        //find customer base on user id
                        var customer = await _customerRepository.GetCustomerByUserIdAsync(userId);

                        if (customer == null) return new ApiResponse<UserCustomerPostmanDto>("Customer with this userId notfound", 404);

                        return new ApiResponse<UserCustomerPostmanDto>(result.Data.ToUserCustomerPostmanDto(customer.ToDto()));

                    case "Postman":
                        //find postman base on user id
                        var postman = await _postmanRepository.GetPostmanByUserId(userId);
                        if (postman == null) return new ApiResponse<UserCustomerPostmanDto>("Postman with this userId notfound", 404);
                        return new ApiResponse<UserCustomerPostmanDto>(result.Data.ToUserCustomerPostmanDto(postman.ToDto()));

                    default: break;
                }

                return new ApiResponse<UserCustomerPostmanDto>(result.Data.ToUserCustomerPostmanDto());
            }
            catch (Exception e)
            {
                return new ApiResponse<UserCustomerPostmanDto>(e.Message, 500);
            }
        }

        public async Task<ApiResponse<List<UserDto>>> GetAllUsers()
        {
            try
            {
                return await _context.GetFromJsonAsync<ApiResponse<List<UserDto>>>("/api/users/getAllUsers") ?? throw new Exception("API response is null");
            }
            catch (Exception e)
            {
                return new ApiResponse<List<UserDto>>(e.Message, 500);
            }
        }
    }
}
