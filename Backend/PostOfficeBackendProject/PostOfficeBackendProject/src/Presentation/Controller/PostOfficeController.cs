using CommonDll.Dto;
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
            if(!ModelState.IsValid) return BadRequest(new ApiResponse<object>(ModelState, 400));
            var postOffices = await _repository.GetAllPostsAsync();
            var postOfficesDto = postOffices.Select(x => x.ToBasicInformationDto());
            return Ok(new ApiResponse<object>(postOfficesDto));
        }

        [HttpGet(getByIdRequest)]
        public async Task<IActionResult> GetById([FromRoute] int id) 
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse<object>(ModelState, 400));

            var postOffice = await _repository.GetPostByIdAsync(id);
            if(postOffice == null) return NotFound(new ApiResponse<object>("The Information Not Found", 404));

            return Ok(new ApiResponse<object>(postOffice.ToDto()));
        }

        [HttpPost(createRequest)]
        public async Task<IActionResult> Create([FromBody] PostOfficeUpdateAndCreateDto createDto)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse<object>(ModelState, 400));

            var postOffice = createDto.ToModelFromPostOfficeUpdateAndCreateDto();
            var createdPostOffice = await _repository.CreatePostAsync(postOffice);
            var createdPostOfficeDto = createdPostOffice.ToDto();

            return Ok(new ApiResponse<object>(createdPostOfficeDto));
        }

        [HttpPut(updateRequest)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] PostOfficeUpdateAndCreateDto updateDto) 
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse<object>(ModelState, 400));

            var postOffice = await _repository.UpdatePostAsync(id, updateDto.ToModelFromPostOfficeUpdateAndCreateDto());
            if (postOffice == null) return NotFound(new ApiResponse<object>("The Information Not Found", 404));

            return Ok(new ApiResponse<object>(postOffice.ToDto()));
        }
    }
}
