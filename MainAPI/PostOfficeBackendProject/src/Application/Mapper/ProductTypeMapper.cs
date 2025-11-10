using CommonDll.Dto;
using PostOfficeBackendProject.src.Domain.Model;

namespace PostOfficeBackendProject.src.Application.Mapper
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
