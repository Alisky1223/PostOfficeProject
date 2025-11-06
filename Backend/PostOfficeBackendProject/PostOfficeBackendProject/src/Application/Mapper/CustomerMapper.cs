using CommonDll.Dto;
using PostOfficeBackendProject.src.Domain.Model;

namespace PostOfficeBackendProject.src.Application.Mapper
{
    public static class CustomerMapper
    {
        public static CustomerDto ToDto(this Customer customer)
        {
            return new CustomerDto
            {
                Id = customer.Id,
                CustomerNumber = customer.CustomerNumber,
                Products = customer.Products.Select(p => p.ToDto()).ToList(),
                UserId = customer.UserId
            };
        }

        public static CustomerBasicInformationDto ToBasicInformationDto(this Customer customer)
        {
            return new CustomerBasicInformationDto
            {
                Id = customer.Id,
                CustomerNumber = customer.CustomerNumber,
                UserId = customer.UserId,
            };
        }

        public static Customer ToCustomerFromCreateDto(this CustomerUpdateAndCreateDto createDto)
        {
            return new Customer
            {
                CustomerNumber = createDto.CustomerNumber,
                UserId = createDto.UserId
            };
        }
    }
}
