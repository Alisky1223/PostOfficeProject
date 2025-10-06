using CommonDll.Dto;
using PostOfficeBackendProject.src.Domain.Model;

namespace PostOfficeBackendProject.src.Application.Mapper
{
    public static class ProductMapper
    {
        public static ProductDto ToDto(this Product product) 
        {
            return new ProductDto 
            {
                Description = product.Description,
                Id = product.Id,
                Price = product.Price,
                ProductName = product.ProductName
            };
        }


        public static Product ToProductFromCreateDto(this ProductUpdateAndCreateDto createDto) 
        {
            return new Product
            {
                Description = createDto.Description,
                Price = createDto.Price,
                ProductName = createDto.ProductName,
                PostOfficeId = createDto.PostOfficeId,
                ProductTypeId = createDto.ProductTypeId,
            };
        }
    }
}
