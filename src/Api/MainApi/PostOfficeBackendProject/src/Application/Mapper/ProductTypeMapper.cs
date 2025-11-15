using CommonDll.Dto;
using PostOfficeProject.Core.src.Domain.Model;

namespace PostOfficeProject.Core.src.Application.Mapper
{
    public static class ProductTypeMapper
    {
        public static ProductTypeDto ToDto(this ProductType productType) 
        {
            return new ProductTypeDto 
            {
                Id = productType.Id,
                Type = productType.Type,
            };
        }

        public static ProductType ToProductTypeFromCreateDto(this ProductTypeUpdateAndCreateDto createDto) 
        {
            return new ProductType 
            {
                Type = createDto.Type,
            };
        }
    }
}
