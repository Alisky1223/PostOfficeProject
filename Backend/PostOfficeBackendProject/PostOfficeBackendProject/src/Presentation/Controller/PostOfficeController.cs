using Microsoft.AspNetCore.Mvc;
using PostOfficeBackendProject.src.Application.Dto;
using PostOfficeBackendProject.src.Application.Mapper;
using PostOfficeBackendProject.src.Domain.Interface;

namespace PostOfficeBackendProject.src.Presentation.Controller
{
    [Route("api/postOffice")]
    [ApiController]
    public class PostOfficeController : ControllerBase
    {
        private const string getAllRequest      = "getAllPostOffice";
        private const string getByIdRequest     = "getByIdPostOffice/{id:int}";
        private const string updateRequest      = "updatePostOffice/{id:int}";
        private const string createRequest      = "createPostOffice";
        
        private readonly IPostOfficeRepository _repository;

        public PostOfficeController(IPostOfficeRepository repository)
        {
            _repository = repository;
        }

        [HttpGet(getAllRequest)]
        public async Task<IActionResult> GetAll() 
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var postOffices = await _repository.GetAllPostsAsync();
            var postOfficesDto = postOffices.Select(x => x.ToDto());
            return Ok(postOfficesDto);
        }

        [HttpPost(createRequest)]
        public async Task<IActionResult> Create([FromBody] PostOfficeUpdateAndCreateDto createDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var postOffice = createDto.ToModelFromPostOfficeUpdateAndCreateDto();
            var createdPostOffice = await _repository.CreatePostAsync(postOffice);
            var createdPostOfficeDto = createdPostOffice.ToDto();
            return Ok(createdPostOfficeDto);
        }
    }
}
