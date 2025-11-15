using CommonDll.Dto;
using Microsoft.AspNetCore.Mvc;
using PostOfficeProject.Core.src.Application.Mapper;
using PostOfficeProject.Core.src.Domain.Interface;

namespace PostOfficeProject.Core.src.Presentation.Controller
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private const string getallRequest = "getall";
        private const string createRequest = "create";
        private const string updateRequest = "update/{id:int}";
        private const string getbyIdRequest = "getbyId/{id:int}";

        private readonly IProductRepository _repository;
        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet(getallRequest)]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse<object>(ModelState, 400));
            var products = await _repository.GetAllAsync();
            var productsDto = products.Select(x => x.ToBasicInformationDto()).ToList();

            return Ok(new ApiResponse<object>(productsDto));
        }

        [HttpGet(getbyIdRequest)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse<object>(ModelState, 400));
            var product = await _repository.GetById(id);

            if (product == null) return NotFound(new ApiResponse<object>("The Information Not Found", 404));

            return Ok(new ApiResponse<object>(product.ToDto()));
        }

        [HttpPost(createRequest)]
        public async Task<IActionResult> Create([FromBody] ProductUpdateAndCreateDto createDto)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse<object>(ModelState, 400));

            var newProduct = await _repository.CreateAsync(createDto.ToProductFromCreateDto());

            return Ok(new ApiResponse<object>(newProduct.ToDto()));
        }

        [HttpPut(updateRequest)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ProductUpdateAndCreateDto updateDto)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse<object>(ModelState, 400));

            var updatedProduct = await _repository.UpdateAsync(id, updateDto.ToProductFromCreateDto());
            if (updatedProduct == null) return NotFound(new ApiResponse<object>("The Information Not Found", 404));

            return Ok(new ApiResponse<object>(updatedProduct.ToDto()));
        }
    }
}
