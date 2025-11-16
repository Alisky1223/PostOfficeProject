using CommonDll.Dto;
using PostOfficeProject.Core.src.Application.Dto;
using PostOfficeProject.Core.src.Domain.Model;

namespace PostOfficeProject.Core.src.Application.Mapper
{
    public static class PostOfficeMapper
    {
        public static PostOfficeDto ToDto(this PostOffice postOffice)
        {
            return new PostOfficeDto
            {
                Id                  = postOffice.Id,
                OfficeName          = postOffice.OfficeName,
                OfficeAccessCode    = postOffice.OfficeAccessCode,
                Address             = postOffice.Address,
                StorageCapacity     = postOffice.StorageCapacity,
                Products            = postOffice.Products.Select(x => x.ToBasicInformationDto()).ToList(),
                PostMans            = postOffice.Postman.Select(x => x.ToBasicInformationDto()).ToList(),
                Transport           = postOffice.Transport.Select(x => x.ToDto()).ToList(),
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

        public static PostOfficeBasicInformationDto ToBasicInformationDto(this PostOffice postOffice) 
        {
            return new PostOfficeBasicInformationDto 
            {
                Id = postOffice.Id,
                OfficeName = postOffice.OfficeName,
                OfficeAccessCode = postOffice.OfficeAccessCode,
                Address = postOffice.Address,
                StorageCapacity = postOffice.StorageCapacity,
            };
        }
    }
}
