using PostOfficeBackendProject.src.Application.Dto;
using PostOfficeBackendProject.src.Domain.Model;

namespace PostOfficeBackendProject.src.Application.Mapper
{
    public static class PostOfficeMapper
    {
        public static PostOfficeDto ToDto(this PostOffice postOffice)
        {
            return new PostOfficeDto
            {
                OfficeName = postOffice.OfficeName,
                OfficeAccessCode = postOffice.OfficeAccessCode,
                Address = postOffice.Address,
                StorageCapacity = postOffice.StorageCapacity,
            };
        }

        public static PostOffice ToModelFromPostOfficeUpdateAndCreateDto(this PostOfficeUpdateAndCreateDto createDto) 
        {
            return new PostOffice
            {
                OfficeName = createDto.OfficeName,
                OfficeAccessCode = createDto.OfficeAccessCode,
                Address = createDto.Address,
                StorageCapacity = createDto.StorageCapacity,
            };
        }
    }
}
