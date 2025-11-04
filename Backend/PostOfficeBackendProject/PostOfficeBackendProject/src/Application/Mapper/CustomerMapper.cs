using CommonDll.Dto;
using PostOfficeBackendProject.src.Domain.Model;

namespace PostOfficeBackendProject.src.Application.Mapper
{
    public static class CustomerMapper
    {
        public static CustomerDto ToDto(this Customer customer, UserPersonalInformationDto userPersonalInformation)
        {
            return new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name,
                CustomerNumber = customer.CustomerNumber,
                PersonalInformation = userPersonalInformation,
                Products = customer.Products.Select(p => p.ToDto()).ToList()
            };
        }

        public static CustomerBasicInformationDto ToBasicInformationDto(this Customer customer)
        {
            return new CustomerBasicInformationDto
            {
                Id = customer.Id,
                Name = customer.Name,
                CustomerNumber = customer.CustomerNumber
            };
        }

        public static Customer ToCustomerFromCreateDto(this CustomerUpdateAndCreateDto createDto)
        {
            return new Customer
            {
                Name = createDto.Name,
                CustomerNumber = createDto.CustomerNumber,
            };
        }
    }
}
