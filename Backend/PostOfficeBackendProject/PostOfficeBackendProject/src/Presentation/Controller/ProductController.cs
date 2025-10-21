using CommonDll.Dto;
using Microsoft.AspNetCore.Mvc;
using PostOfficeBackendProject.src.Application.Mapper;
using PostOfficeBackendProject.src.Domain.Interface;
using System.Threading.Tasks;

namespace PostOfficeBackendProject.src.Presentation.Controller
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
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var products = await _repository.GetAllAsync();
            var productsDto = products.Select(x => x.ToDto()).ToList();

            return Ok(productsDto);
        }

        [HttpGet(getbyIdRequest)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var product = await _repository.GetById(id);
            if (product == null) return NotFound(id);
            return Ok(product.ToDto());
        }

        [HttpPost(createRequest)]
        public async Task<IActionResult> Create([FromBody] ProductUpdateAndCreateDto createDto) 
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var newProduct = await _repository.CreateAsync(createDto.ToProductFromCreateDto());
            return Ok(newProduct.ToDto());
        }

        [HttpPut(updateRequest)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ProductUpdateAndCreateDto updateDto) 
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var updatedProduct = await _repository.UpdateAsync(id, updateDto.ToProductFromCreateDto());
            if(updatedProduct == null) return NotFound(updateDto);
            return Ok(updatedProduct.ToDto());
        }
    }
}
