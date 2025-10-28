using CommonDll.Dto;
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

        public async Task<List<PostManDto>> GetAllPostMansAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<PostManDto>>("api/postman/getall");
        }

        public async Task<PostManDto?> UpdatePostManAsync(int id, PostmanUpdateAndCreateDto update)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/postman/update/{id}", update);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<PostManDto>();
            }
            return null;
        }

        public async Task<PostManDto?> CreatePostManAsync(PostmanUpdateAndCreateDto create)
        {
            var response = await _httpClient.PostAsJsonAsync("api/postman/create", create);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<PostManDto>();
            }
            return null;
        }

        public async Task<PostManDto?> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<PostManDto>($"api/postman/getbyId/{id}");
        }
    }
}
