using CommonDll.Dto;
using Microsoft.AspNetCore.Mvc;
using PostOfficeBackendProject.src.Application.Mapper;
using PostOfficeBackendProject.src.Domain.Interface;

namespace PostOfficeBackendProject.src.Presentation.Controller
{
    [Route("api/TransportStatus")]
    [ApiController]
    public class TransportStatusController: ControllerBase
    {
        private const string getallRequest = "getall";
        private const string createRequest = "create";
        private const string getByIdRequest = "getbyId/{id:int}";
        private const string updateRequest = "update/{id:int}";

        private readonly ITransportStatusRepository _repository;
        public TransportStatusController(ITransportStatusRepository repository)
        {
            _repository = repository;
        }

        [HttpGet(getallRequest)]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var transportStatuses = await _repository.GetAll();
            var transportStatusesDto = transportStatuses.Select(x => x.ToDto());

            return Ok(transportStatusesDto);
        }

        [HttpGet(getByIdRequest)]
        public async Task<IActionResult> GetById([FromRoute] int id) 
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var transportStatus = await _repository.GetById(id);
            if (transportStatus == null) return NotFound();

            return Ok(transportStatus.ToDto());
        }

        [HttpPost(createRequest)]
        public async Task<IActionResult> Create([FromBody] TransportStatusUpdateAndCreateDto createDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var transportStatus = createDto.ToTransportStatusFromCreateDto();
            var createdTransportStatus = await _repository.Create(transportStatus);

            return Ok(createdTransportStatus.ToDto());
        }

        [HttpGet(updateRequest)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] TransportStatusUpdateAndCreateDto updateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var transportStatus = await _repository.Update(id, updateDto.ToTransportStatusFromCreateDto());
            if (transportStatus == null) return NotFound();

            return Ok(transportStatus.ToDto());
        }
    }
}
