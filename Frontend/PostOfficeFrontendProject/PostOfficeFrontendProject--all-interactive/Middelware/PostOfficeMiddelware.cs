using Azure;
using CommonDll.Dto;
using PostOfficeBackendProject.src.Application.Dto;
using PostOfficeFrontendProject__all_interactive.Interface;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace PostOfficeFrontendProject__all_interactive.Middelware
{
    public class PostOfficeMiddelware : IPostOfficeMiddelware
    {
        private readonly HttpClient _httpClient;

        public PostOfficeMiddelware(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<List<PostOfficeBasicInformationDto>>> GetAllPostOfficesAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<ApiResponse<List<PostOfficeBasicInformationDto>>>("/api/postOffice/getAllPostOffice") ?? throw new Exception ("API Response Is Null") ;

            }
            catch (Exception e)
            {
                return new ApiResponse<List<PostOfficeBasicInformationDto>>(e.Message, 500);
            }
        
        }

        public async Task<ApiResponse<PostOfficeDto>> GetByIdAsync(int id)
        {


            try
            {
                return await _httpClient.GetFromJsonAsync<ApiResponse<PostOfficeDto>>($"api/postOffice/getByIdPostOffice/{id}") ?? throw new Exception("API Response Is Null");

            }
            catch (Exception e)
            {
                return new ApiResponse<PostOfficeDto>(e.Message, 500);
            }
           
        }

        public async Task<ApiResponse<PostOfficeDto>> UpdatePostOfficeAsync(int id, PostOfficeUpdateAndCreateDto updateDto)
        {

            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/postOffice/updatePostOffice/{id}", updateDto);

                return await HandleResponse<ApiResponse<PostOfficeDto>>(response, "Update failed");
            }
            catch (Exception e)
            {
                return new ApiResponse<PostOfficeDto>(e.Message, 500);
            }

        }

        public async Task<ApiResponse<PostOfficeDto>> CreatePostOfficeAsync(PostOfficeUpdateAndCreateDto createDto)
        {

            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/postOffice/createPostOffice", createDto);

                return await HandleResponse<ApiResponse<PostOfficeDto>>(response, "Create failed");
            }
            catch (Exception e)
            {
                return new ApiResponse<PostOfficeDto>(e.Message, 500);
            }

        }

        private static async Task<T> HandleResponse<T>(HttpResponseMessage response, string action)
        {
            var result = await response.Content.ReadFromJsonAsync<T>(
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result ?? throw new InvalidOperationException($"{action}: Deserialized response is null.");
        }
   
    }

}





