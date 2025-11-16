using CommonDll.Dto;
using PostOfficeProject.Core.src.Domain.Model;

namespace PostOfficeProject.Core.src.Application.Mapper
{
    public static class TransportMapper
    {
        public static TransportDto ToDto (this Transport transport) 
        {
            return new TransportDto
            {
                Id = transport.Id,
                DeliverdDate = transport.DeliverdDate,
                DeliverCode = transport.DeliverCode,
                Postman = transport.Postman?.ToBasicInformationDto() ?? null,
                PostOffice = transport.PostOffice?.ToBasicInformationDto() ?? null,
                Product = transport.Product?.ToBasicInformationDto() ?? null,
                Customer = transport.Customer?.ToBasicInformationDto() ?? null
            };
        }

        public static TransportBasicInformationDto ToBasicInformationDto (this Transport transport) 
        {
            return new TransportBasicInformationDto
            {
                Id = transport.Id,
                DeliverdDate = transport.DeliverdDate,
                DeliverCode = transport.DeliverCode
            };
        }

        public static Transport ToTransportFromUpdateAndCreateDto(this TransportUpdateAndCreateDto createDto) 
        {
            return new Transport 
            {
                DeliverCode = createDto.DeliverCode,
                DeliverdDate = createDto.DeliverdDate,
                PostmanId = createDto.PostmanId,
                ProductId = createDto.ProductId,
                PostOfficeId = createDto.PostOfficeId,
                CustomerId = createDto.CustomerId
            };
        }
    }
}
