using CommonDll.Dto;
using Microsoft.AspNetCore.Mvc;
using PostOfficeBackendProject.src.Application.Mapper;
using PostOfficeBackendProject.src.Domain.Interface;

namespace PostOfficeBackendProject.src.Presentation.Controller
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
        public CustomerController(ICustomerRepository repository)
        {
            _repository = repository;
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
        public async Task<IActionResult> GetById(int id)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse<object>(ModelState, 400));

            var customer = await _repository.GetCustomerByIdAsync(id);

            if (customer == null) return NotFound(new ApiResponse<object>("The Information Not Found", 404));

            return Ok(new ApiResponse<object>(customer.ToDto()));
        }

        [HttpPost(createCustomerRequestRoute)]
        public async Task<IActionResult> CreateAsync([FromBody] CustomerUpdateAndCreateDto createDto)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse<object>(ModelState, 400));

            var customer = createDto.ToCustomerFromCreateDto();
            var createdCustomer = await _repository.CreateAsync(customer);

            return Ok(new ApiResponse<object>(createdCustomer.ToDto()));
        }

        [HttpPut(updateCustomerRequestRoute)]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] CustomerUpdateAndCreateDto updateDto)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse<object>(ModelState, 400));

            var customer = updateDto.ToCustomerFromCreateDto();
            var updatedCustomer = await _repository.UpdateCustomerAsync(id, customer);

            if (updatedCustomer == null) return NotFound(new ApiResponse<object>("The Information Not Found",404));

            return Ok(new ApiResponse<object>(updatedCustomer.ToDto()));
        }
    }
}
