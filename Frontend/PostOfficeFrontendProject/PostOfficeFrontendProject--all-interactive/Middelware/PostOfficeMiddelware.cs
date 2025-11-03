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
                ApiResponse<List<PostOfficeBasicInformationDto>>? apiResponse = await _httpClient.GetFromJsonAsync<ApiResponse<List<PostOfficeBasicInformationDto>>>("api/postOffice/getAllPostOffice");

                if (apiResponse == null)
                {
                    return new ApiResponse<List<PostOfficeBasicInformationDto>>(
                        new List<string> { "پاسخ نامعتبر از سرور" },
                        (int)400
                    );
                }

                return apiResponse;
            }
            catch (HttpRequestException ex) when (ex.StatusCode != null)
            {
                // HTTP error (مثل 404 یا 500)
                var statusCode = (int)ex.StatusCode;
                return new ApiResponse<List<PostOfficeBasicInformationDto>>(
                    new List<string> { $"{ex.Message} (Status: {statusCode})" },
                    statusCode
                );
            }
            catch (HttpRequestException ex)
            {
                // Network error
                return new ApiResponse<List<PostOfficeBasicInformationDto>>(
                    "خطای شبکه در فراخوانی API",
                    0
                );
            }
            catch (JsonException ex)
            {
                // Deserialize fail
                return new ApiResponse<List<PostOfficeBasicInformationDto>>(
                    $"خطا در خواندن پاسخ JSON: {ex.Message}",
                    0
                );
            }
            catch (Exception ex)
            {
                // Unexpected
                return new ApiResponse<List<PostOfficeBasicInformationDto>>(
                    $"خطای غیرمنتظره: {ex.Message}",
                    0
                );
            }
        }

        public async Task<ApiResponse<PostOfficeDto>> GetByIdAsync(int id)
        {
            try
            {

                ApiResponse<PostOfficeDto>? apiResponse = await _httpClient.GetFromJsonAsync<ApiResponse<PostOfficeDto>>($"api/postOffice/getByIdPostOffice/{id}");

                if (apiResponse == null)
                {
                    return new ApiResponse<PostOfficeDto>(
                        new List<string> { "پاسخ نامعتبر از سرور" },
                        (int)400
                    );
                }

                return apiResponse;

            }

            catch (HttpRequestException ex) when (ex.StatusCode != null)
            {
                // HTTP error (مثل 404 یا 500)
                var statusCode = (int)ex.StatusCode;
                return new ApiResponse<PostOfficeDto>(
                    new List<string> { $"{ex.Message} (Status: {statusCode})" },
                    statusCode
                );
            }
            catch (HttpRequestException ex)
            {
                // Network error
                return new ApiResponse<PostOfficeDto>(
                    "خطای شبکه در فراخوانی API",
                    0
                );
            }
            catch (JsonException ex)
            {
                // Deserialize fail
                return new ApiResponse<PostOfficeDto>(
                    $"خطا در خواندن پاسخ JSON: {ex.Message}",
                    0
                );
            }
            catch (Exception ex)
            {
                // Unexpected
                return new ApiResponse<PostOfficeDto>(
                    $"خطای غیرمنتظره: {ex.Message}",
                    0
                );
            }
        }

        public async Task<ApiResponse<PostOfficeDto>> UpdatePostOfficeAsync(int id, PostOfficeUpdateAndCreateDto updateDto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/postOffice/updatePostOffice/{id}", updateDto);

                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<PostOfficeDto>>();

                if (apiResponse == null)
                {
                    return new ApiResponse<PostOfficeDto>(
                        new List<string> { "پاسخ نامعتبر از سرور" },
                        (int)400
                    );
                }

                return apiResponse;
            }

            catch (HttpRequestException ex) when (ex.StatusCode != null)
            {
                // HTTP error (مثل 404 یا 500)
                var statusCode = (int)ex.StatusCode;
                return new ApiResponse<PostOfficeDto>(
                    new List<string> { $"{ex.Message} (Status: {statusCode})" },
                    statusCode
                );
            }

            catch (HttpRequestException)
            {
                // Network error
                return new ApiResponse<PostOfficeDto>(
                    "خطای شبکه در فراخوانی API",
                    0
                );
            }

            catch (JsonException ex)
            {
                // Deserialize fail
                return new ApiResponse<PostOfficeDto>(
                    $"خطا در خواندن پاسخ JSON: {ex.Message}",
                    0
                );
            }

            catch (Exception ex)
            {
                // Unexpected
                return new ApiResponse<PostOfficeDto>(
                    $"خطای غیرمنتظره: {ex.Message}",
                    0
                );
            }
        }

        public async Task<ApiResponse<PostOfficeDto>> CreatePostOfficeAsync(PostOfficeUpdateAndCreateDto createDto)
        {
            try
            {
                // ارسال درخواست POST
                var response = await _httpClient.PostAsJsonAsync("api/postOffice/createPostOffice", createDto);

                // خواندن پاسخ و تبدیل به ApiResponse<PostOfficeDto>
                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<PostOfficeDto>>();

                // بررسی null بودن پاسخ
                if (apiResponse == null)
                {
                    return new ApiResponse<PostOfficeDto>(
                        new List<string> { "پاسخ نامعتبر از سرور" },
                        (int)response.StatusCode
                    );
                }

                return apiResponse;
            }
            catch (HttpRequestException ex) when (ex.StatusCode != null)
            {
                // HTTP error (مثل 404 یا 500)
                var statusCode = (int)ex.StatusCode;
                return new ApiResponse<PostOfficeDto>(
                    new List<string> { $"{ex.Message} (Status: {statusCode})" },
                    statusCode
                );
            }
            catch (HttpRequestException)
            {
                // Network error
                return new ApiResponse<PostOfficeDto>(
                    "خطای شبکه در فراخوانی API",
                    0
                );
            }
            catch (JsonException ex)
            {
                // Deserialize fail
                return new ApiResponse<PostOfficeDto>(
                    $"خطا در خواندن پاسخ JSON: {ex.Message}",
                    0
                );
            }
            catch (Exception ex)
            {
                // Unexpected
                return new ApiResponse<PostOfficeDto>(
                    $"خطای غیرمنتظره: {ex.Message}",
                    0
                );
            }
        }

    }
}
