using CommonDll.Dto;

namespace PostOfficeProject.Core.src.Application.Mapper
{
    public static class UserInformationMapper
    {
        public static UserCustomerPostmanDto ToUserCustomerPostmanDto(this UserPersonalInformationDto personalInformationDto, CustomerDto customerDto)
        {
            return new UserCustomerPostmanDto
            {
                PersonalCode = customerDto.CustomerNumber,
                Products = customerDto.Products,
                FirstName = personalInformationDto.FirstName,
                LastName = personalInformationDto.LastName,
                UserEmail = personalInformationDto.UserEmail,
                UserPhone = personalInformationDto.UserPhone,
                Username = personalInformationDto.Username,
            };
        }

        public static UserCustomerPostmanDto ToUserCustomerPostmanDto(this UserPersonalInformationDto personalInformationDto, PostManDto postmanDto)
        {
            return new UserCustomerPostmanDto
            {
                FirstName = personalInformationDto.FirstName,
                LastName = personalInformationDto.LastName,
                PersonalCode = postmanDto.PersonalCode,
                Products = postmanDto.Products,
                UserEmail = personalInformationDto.UserEmail,
                Username = personalInformationDto.Username,
                UserPhone = personalInformationDto.UserPhone
            };
        }

        public static UserCustomerPostmanDto ToUserCustomerPostmanDto(this UserPersonalInformationDto personalInformationDto)
        {
            return new UserCustomerPostmanDto
            {
                FirstName = personalInformationDto.FirstName,
                LastName = personalInformationDto.LastName,
                UserEmail = personalInformationDto.UserEmail,
                Username = personalInformationDto.Username,
                UserPhone = personalInformationDto.UserPhone,
            };
        }
    }
}
