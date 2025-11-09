using CommonDll.Dto;
using PostOfficeBackendProject.src.Domain.Model;

namespace PostOfficeBackendProject.src.Application.Mapper
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
