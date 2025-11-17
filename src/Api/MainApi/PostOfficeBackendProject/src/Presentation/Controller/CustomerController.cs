using CommonDll.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostOfficeProject.Core.src.Application.Mapper;
using PostOfficeProject.Core.src.Domain.Interface;
using PostOfficeProject.Core.src.Domain.Model;

namespace PostOfficeProject.Core.src.Presentation.Controller
{
    [Route("api/Customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private const string createCustomerRequestRoute = "CreateCustomer";
        private const string getAllCustomersRequestRoute = "GetAllCustomers";
        private const string getCustomerByIdRequestRoute = "GetCustomerById/{id}";
        private const string updateCustomerRequestRoute = "UpdateCustomer/{id}";

        private readonly ICustomerRepository _repository;
        private readonly IUsersMiddleware _middleware;
        public CustomerController(ICustomerRepository repository, IUsersMiddleware middleware)
        {
            _repository = repository;
            _middleware = middleware;
        }

        [HttpGet(getAllCustomersRequestRoute)]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse<object>(ModelState, 400));

            var customers = await _repository.GetAllCustomersAsync();
            var customersDto = customers.Select(s => s.ToBasicInformationDto());

            return Ok(new ApiResponse<object>(customersDto));
        }

        [HttpGet(getCustomerByIdRequestRoute)]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse<object>(ModelState, 400));

            var customer = await _repository.GetCustomerByIdAsync(id);

            if (customer == null) return NotFound(new ApiResponse<object>("The Information Not Found", 404));

            var postmanUserResult = await _middleware.GetUserInformation(customer.UserId);

            return Ok(postmanUserResult);

        }

        [HttpPost(createCustomerRequestRoute)]
        public async Task<IActionResult> CreateAsync([FromBody] CustomerUpdateAndCreateDto createDto)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse<object>(ModelState, 400));

            var customer = createDto.ToCustomerFromCreateDto();
            await _repository.CreateAsync(customer);

            return Ok(new ApiResponse<object>("Done"));//createdCustomer.ToDto()
        }

        [HttpPut(updateCustomerRequestRoute)]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] CustomerUpdateAndCreateDto updateDto)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse<object>(ModelState, 400));

            var customer = updateDto.ToCustomerFromCreateDto();
            var updatedCustomer = await _repository.UpdateCustomerAsync(id, customer);

            if (updatedCustomer == null) return NotFound(new ApiResponse<object>("The Information Not Found", 404));

            return Ok(new ApiResponse<object>("Done"));//updatedCustomer.ToDto()
        }
    }
}
