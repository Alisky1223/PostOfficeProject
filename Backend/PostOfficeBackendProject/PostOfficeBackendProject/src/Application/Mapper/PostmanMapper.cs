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
                Name = postman.Name,
                PersonalCode = postman.PersonalCode,  
            };
        }

        public static Postman ToPostmanFromCreateAndUpdateDto(this PostmanUpdateAndCreateDto createDto) 
        {
            return new Postman 
            {
                Name = createDto.Name,
                PersonalCode = createDto.PersonalCode,
                PostOfficeId = createDto.PostOfficeId,
            };
        }
    }
}
