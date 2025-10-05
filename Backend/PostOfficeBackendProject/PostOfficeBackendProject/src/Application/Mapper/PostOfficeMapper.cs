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
                OfficeAccessCode = postOffice.OfficeAccessCode
            };
        }

        public static PostOffice ToModelFromPostOfficeUpdateAndCreateDto(this PostOfficeUpdateAndCreateDto createDto) 
        {
            return new PostOffice
            {
                OfficeName = createDto.OfficeName,
                OfficeAccessCode = createDto.OfficeAccessCode
            };
        }
    }
}
