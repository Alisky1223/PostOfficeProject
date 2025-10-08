using CommonDll.Dto;
using Microsoft.AspNetCore.Mvc;
using PostOfficeBackendProject.src.Application.Mapper;
using PostOfficeBackendProject.src.Domain.Interface;

namespace PostOfficeBackendProject.src.Presentation.Controller
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
        public PostmanController(IPostmanRepository repository)
        {
            _repository = repository;
        }

        [HttpGet(getAllRequest)]
        public async Task<IActionResult> GetAll() 
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var postmen = await _repository.GetPostmen();
            var postmenDto = postmen.Select(x => x.ToDto()).ToList();

            return Ok(postmenDto);
        }

        [HttpGet(getByIdRequest)]
        public async Task<IActionResult> GetById([FromRoute] int id) 
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var postMan = await _repository.GetPostmanById(id);
            if (postMan == null) return NotFound();
            
            return Ok(postMan.ToDto());
        }

        [HttpPost(createRequest)]
        public async Task<IActionResult> Create([FromBody] PostmanUpdateAndCreateDto createDto) 
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var newPostman = await _repository.Create(createDto.ToPostmanFromCreateAndUpdateDto());
            return Ok(newPostman.ToDto());
        }

        [HttpPut(updateRequest)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] PostmanUpdateAndCreateDto updateDto) 
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var targetPostman = await _repository.Update(id, updateDto.ToPostmanFromCreateAndUpdateDto());
            if(targetPostman == null) return NotFound();    

            return Ok(targetPostman.ToDto());
        }
    }
}
