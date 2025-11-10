using CommonDll.Dto;
using Microsoft.AspNetCore.Mvc;
using PostOfficeBackendProject.src.Application.Mapper;
using PostOfficeBackendProject.src.Domain.Interface;

namespace PostOfficeBackendProject.src.Presentation.Controller
{
    [Route("api/productType")]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {
        private const string getallRequest = "getAll";
        private const string createRequest = "create";
        private const string updateRequest = "update/{id:int}";

        private readonly IProductTypeRepository _repository;
        public ProductTypeController(IProductTypeRepository repository)
        {
            _repository = repository;
        }

        [HttpGet(getallRequest)]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse<object>(ModelState, 400));

            var allProductTypes = await _repository.GetAllProductTypesAsync();
            var allProductTypesDto = allProductTypes.Select(x => x.ToDto()).ToList();

            return Ok(new ApiResponse<object>(allProductTypesDto));
        }

        [HttpPost(createRequest)]
        public async Task<IActionResult> Create([FromBody] ProductTypeUpdateAndCreateDto createDto)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse<object>(ModelState, 400));

            var createProductType = await _repository.CreateProductType(createDto.ToProductTypeFromCreateDto());
            return Ok(new ApiResponse<object>(createProductType.ToDto()));
        }

        [HttpPut(updateRequest)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ProductTypeUpdateAndCreateDto updateDto)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse<object>(ModelState, 400));

            var targetUpdated = await _repository.UpdateProductTypeAsync(id, updateDto.ToProductTypeFromCreateDto());
            if (targetUpdated == null) return NotFound(new ApiResponse<object>("The Information Not Found", 404));

            return Ok(new ApiResponse<object>(targetUpdated.ToDto()));
        }
    }
}
