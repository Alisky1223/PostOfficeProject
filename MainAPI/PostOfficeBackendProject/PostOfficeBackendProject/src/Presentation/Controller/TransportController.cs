using CommonDll.Dto;
using Microsoft.AspNetCore.Mvc;
using PostOfficeBackendProject.src.Application.Mapper;
using PostOfficeBackendProject.src.Domain.Interface;

namespace PostOfficeBackendProject.src.Presentation.Controller
{
    [Route("api/transport")]
    [ApiController]
    public class TransportController : ControllerBase
    {
        private const string getAllRequest = "getall";
        private const string createRequest = "create";
        private const string updateRequest = "update/{id:int}";
        private const string getByIdRequest = "getbyId/{id:int}";

        private readonly ITransportRepository _repository;
        public TransportController(ITransportRepository repository)
        {
            _repository = repository;
        }

        [HttpGet(getAllRequest)]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse<object>(ModelState, 400));

            var transports = await _repository.GetAllAsync();
            var transportsDto = transports.Select(x => x.ToDto());
            return Ok(new ApiResponse<object>(transportsDto));
        }

        [HttpGet(getByIdRequest)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse<object>(ModelState, 400));

            var transport = await _repository.GetByIdAsync(id);
            if (transport == null) return NotFound(new ApiResponse<object>("The Information Not Found", 404));

            return Ok(new ApiResponse<object>(transport.ToDto()));
        }

        [HttpPost(createRequest)]
        public async Task<IActionResult> Create([FromBody] TransportUpdateAndCreateDto createDto)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse<object>(ModelState, 400));

            var transport = createDto.ToTransportFromUpdateAndCreateDto();
            var createdTransport = await _repository.CreateAsync(transport);
            return Ok(new ApiResponse<object>(createdTransport.ToDto()));
        }

        [HttpPut(updateRequest)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] TransportUpdateAndCreateDto updateDto)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse<object>(ModelState, 400));

            var transport = await _repository.UpdateAsync(id, updateDto.ToTransportFromUpdateAndCreateDto());
            if (transport == null) return NotFound(new ApiResponse<object>("The Information Not Found", 404));

            return Ok(new ApiResponse<object>(transport.ToDto()));
        }
    }
}
