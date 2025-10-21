using PostOfficeBackendProject.src.Application.Dto;
using PostOfficeFrontendProject__all_interactive.Interface;
using System.Net.Http;

namespace PostOfficeFrontendProject__all_interactive.Middelware
{
    public class PostOfficeMiddelware : IPostOfficeMiddelware
    {
        private readonly HttpClient _httpClient;

        public PostOfficeMiddelware(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PostOfficeDto>> GetAllPostOfficesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<PostOfficeDto>>("api/postOffice/getAllPostOffice");
        }

    }
}
