using CommonDll.Dto;
using PostOfficeProject.Core.src.Domain.Model;

namespace PostOfficeProject.Core.src.Application.Mapper
{
    public static class TransportStatusMapper
    {
        public static TransportStatusDto ToDto(this TransportStatus model) 
        {
            return new TransportStatusDto 
            {
                Id = model.Id,
                Status = model.Status,  
            };
        }

        public static TransportStatus ToTransportStatusFromCreateDto(this TransportStatusUpdateAndCreateDto createDto) 
        {
            return new TransportStatus
            {
                Status = createDto.Status,
            };
        }
    }
}
