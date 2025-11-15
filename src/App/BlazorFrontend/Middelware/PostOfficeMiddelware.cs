using CommonDll.Dto;
using PostOfficeFrontendProject__all_interactive.Helper;
using PostOfficeFrontendProject__all_interactive.Interface;
using PostOfficeProject.Core.src.Application.Dto;

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
                return await _httpClient.GetFromJsonAsync<ApiResponse<List<PostOfficeBasicInformationDto>>>("/api/postOffice/getAllPostOffice") ?? throw new Exception("API Response Is Null");

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

                return await ApiHandler.HandleResponse<ApiResponse<PostOfficeDto>>(response, "Update failed");
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

                return await ApiHandler.HandleResponse<ApiResponse<PostOfficeDto>>(response, "Create failed");
            }
            catch (Exception e)
            {
                return new ApiResponse<PostOfficeDto>(e.Message, 500);
            }

        }

    }

}





