using CommonDll.Dto;
using PostOfficeBackendProject.src.Domain.Model;

namespace PostOfficeBackendProject.src.Application.Mapper
{
    public static class PostmanMapper
    {
        public static PostManDto ToDto(this Postman postman) 
        {
            return new PostManDto 
            {
                Id = postman.Id,
                PersonalCode = postman.PersonalCode,  
                Products = postman.Products.Select(x => x.ToDto()).ToList(),
                UserId = postman.UserId,
            };
        }

        public static PostmanBasicInformationDto ToBasicInformationDto(this Postman postman)
        {
            return new PostmanBasicInformationDto
            {
                Id = postman.Id,
                PersonalCode = postman.PersonalCode,
                UserId = postman.UserId
            };
        }

        public static Postman ToPostmanFromCreateAndUpdateDto(this PostmanUpdateAndCreateDto createDto) 
        {
            return new Postman 
            {
                PersonalCode = createDto.PersonalCode,
                PostOfficeId = createDto.PostOfficeId,
                UserId = createDto.UserId
            };
        }
    }
}
