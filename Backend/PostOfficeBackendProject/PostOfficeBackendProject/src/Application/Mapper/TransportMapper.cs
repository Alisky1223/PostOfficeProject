using CommonDll.Dto;
using PostOfficeBackendProject.src.Domain.Model;

namespace PostOfficeBackendProject.src.Application.Mapper
{
    public static class TransportMapper
    {
        public static TransportDto ToDto (this Transport transport) 
        {
            return new TransportDto 
            {
                Id = transport.Id,
                DeliverdDate = transport.DeliverdDate,
                DeliverdTo = transport.DeliverdTo,
                Postman = transport.Postman?.ToBasicInformationDto() ?? null,
                PostOffice = transport.PostOffice?.ToBasicInformationDto() ?? null,
                Product = transport.Product?.ToBasicInformationDto() ?? null
            };
        }

        public static TransportBasicInformationDto ToBasicInformationDto (this Transport transport) 
        {
            return new TransportBasicInformationDto
            {
                Id = transport.Id,
                DeliverdDate = transport.DeliverdDate,
                DeliverdTo = transport.DeliverdTo
            };
        }

        public static Transport ToTransportFromUpdateAndCreateDto(this TransportUpdateAndCreateDto createDto) 
        {
            return new Transport 
            {
                DeliverdTo = createDto.DeliverdTo,
                DeliverdDate = createDto.DeliverdDate,
                PostmanId = createDto.PostmanId,
                ProductId = createDto.ProductId,
                PostOfficeId = createDto.PostOfficeId,
            };
        }
    }
}
