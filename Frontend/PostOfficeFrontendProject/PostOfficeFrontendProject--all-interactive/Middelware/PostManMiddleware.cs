using CommonDll.Dto;
using Microsoft.AspNetCore.Http.HttpResults;
using PostOfficeFrontendProject__all_interactive.Helper;
using PostOfficeFrontendProject__all_interactive.Interface;

namespace PostOfficeFrontendProject__all_interactive.Middelware
{
    public class PostManMiddleware : IPostManMiddleware
    {
        private readonly HttpClient _httpClient;

        public PostManMiddleware(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<List<PostManDto>>> GetAllPostMansAsync()
        {

            try
            {
                return await _httpClient.GetFromJsonAsync<ApiResponse<List<PostManDto>>>("api/postman/getall") ?? throw new Exception("API Response Is Null");

            }
            catch (Exception e)
            {
                return new ApiResponse<List<PostManDto>>(e.Message, 500);
            }

        }

        public async Task<ApiResponse<PostManDto>> UpdatePostManAsync(int id, PostmanUpdateAndCreateDto update)
        {


            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/postman/update/{id}", update);

                return await ApiHandler.HandleResponse<ApiResponse<PostManDto>>(response, "Update failed");
            }
            catch (Exception e)
            {
                return new ApiResponse<PostManDto>(e.Message, 500);
            }

        }

        public async Task<ApiResponse<PostManDto>> CreatePostManAsync(PostmanUpdateAndCreateDto create)
        {

            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/postman/create", create);

                return await ApiHandler.HandleResponse<ApiResponse<PostManDto>>(response, "Create failed");
            }
            catch (Exception e)
            {
                return new ApiResponse<PostManDto>(e.Message, 500);
            }

        }

        public async Task<ApiResponse<PostManDto>> GetByIdAsync(int id)
        {

            try
            {
                return await _httpClient.GetFromJsonAsync<ApiResponse<PostManDto>>($"api/postman/getbyId/{id}") ?? throw new Exception("API Response Is Null");

            }
            catch (Exception e)
            {
                return new ApiResponse<PostManDto>(e.Message, 500);
            }

        }

    }
}
