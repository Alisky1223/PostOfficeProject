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
                ProductName = product.ProductName,
                //ProductType = product.ProductType == null ? null : product.ProductType.ToDto(),
                ProductType = product.ProductType?.ToDto() ?? null,
                TransportStatus = product.TransportStatus?.ToDto() ?? null,
                //PostMan = product.Postman?.ToDto() ?? null
            };
        }

        public static ProductBasicInformationDto ToBasicInformationDto(this Product product)
        {
            return new ProductBasicInformationDto
            {
                Id = product.Id,
                Price = product.Price,
                ProductName = product.ProductName,
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
                TransportStatusId = createDto.TransportStatusId,
                PostmanId = createDto.PostmanId,
            };
        }
    }
}
