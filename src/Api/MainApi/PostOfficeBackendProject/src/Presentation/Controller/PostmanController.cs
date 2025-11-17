using CommonDll.Dto;
using Microsoft.AspNetCore.Mvc;
using PostOfficeProject.Core.src.Application.Mapper;
using PostOfficeProject.Core.src.Domain.Interface;

namespace PostOfficeProject.Core.src.Presentation.Controller
{
    [Route("api/postman")]
    [ApiController]
    public class PostmanController : ControllerBase
    {
        private const string getAllRequest = "getall";
        private const string createRequest = "create";
        private const string getByIdRequest = "getbyId/{id:int}";
        private const string updateRequest = "update/{id:int}";

        private readonly IPostmanRepository _repository;
        private readonly IUsersMiddleware _middleware;

        public PostmanController(IPostmanRepository repository, IUsersMiddleware middleware)
        {
            _repository = repository;
            _middleware = middleware;
        }

        [HttpGet(getAllRequest)]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse<object>(ModelState, 400));

            var postmen = await _repository.GetPostmen();
            var postmenDto = postmen.Select(x => x.ToBasicInformationDto()).ToList();

            return Ok(new ApiResponse<object>(postmenDto));
        }

        [HttpGet(getByIdRequest)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse<object>(ModelState, 400));

            var postMan = await _repository.GetPostmanById(id);
            if (postMan == null) return NotFound(new ApiResponse<object>("The Information Not Found", 404));

            var postmanUserResult = await _middleware.GetUserInformation(postMan.UserId);

            return Ok(postmanUserResult);
        }

        [HttpPost(createRequest)]
        public async Task<IActionResult> Create([FromBody] PostmanUpdateAndCreateDto createDto)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse<object>(ModelState, 400));

            var newPostman = await _repository.Create(createDto.ToPostmanFromCreateAndUpdateDto());
            return Ok(new ApiResponse<object>(newPostman.ToDto()));
        }

        [HttpPut(updateRequest)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] PostmanUpdateAndCreateDto updateDto)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse<object>(ModelState, 400));

            var targetPostman = await _repository.Update(id, updateDto.ToPostmanFromCreateAndUpdateDto());

            if (targetPostman == null) return NotFound(new ApiResponse<object>("The Information Not Found", 404));

            return Ok(new ApiResponse<object>(targetPostman.ToDto()));
        }
    }
}
