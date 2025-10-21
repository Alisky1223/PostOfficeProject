using CommonDll.Dto;
using PostOfficeBackendProject.src.Application.Dto;
using PostOfficeFrontendProject__all_interactive.Interface;
using System.Net.Http;
using System.Net.Http.Headers;

namespace PostOfficeFrontendProject__all_interactive.Middelware
{
    public class PostOfficeMiddelware : IPostOfficeMiddelware
    {
        private readonly HttpClient _httpClient;

        public PostOfficeMiddelware(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PostOfficeBasicInformationDto>> GetAllPostOfficesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<PostOfficeBasicInformationDto>>("api/postOffice/getAllPostOffice");
        }
        public async Task<PostOfficeDto?> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<PostOfficeDto>($"api/postOffice/getByIdPostOffice/{id}");
        }

        public async Task<PostOfficeDto?> UpdatePostOfficeAsync(int id, PostOfficeUpdateAndCreateDto updateDto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/postOffice/updatePostOffice/{id}", updateDto);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<PostOfficeDto>();
            }
            return null;
        }

        public async Task<PostOfficeDto?> CreatePostOfficeAsync(PostOfficeUpdateAndCreateDto createDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/postOffice/createPostOffice", createDto);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<PostOfficeDto>();
            }
            return null;
        }
    }
}
