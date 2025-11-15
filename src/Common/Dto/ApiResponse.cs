namespace CommonDll.Dto
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; } // Made nullable to handle cases where Data is null
        public string? Message { get; set; }
        public List<string> Errors { get; set; }
        public int StatusCode { get; set; } // New property for HTTP status code

        public ApiResponse()
        {
            
        }

        // Constructor for successful response
        public ApiResponse(T data, int statusCode = 200)
        {
            Success = true;
            Data = data;
            Errors = new List<string>();
            StatusCode = statusCode; // Default to 200 OK for success
        }

        // Constructor for error response with single message
        public ApiResponse(string errorMessage, int statusCode)
        {
            Success = false;
            Message = errorMessage;
            Errors = new List<string> { errorMessage };
            StatusCode = statusCode; // Set status code (e.g., 400, 500)
        }

        // Constructor for multiple errors
        public ApiResponse(List<string> errors, int statusCode)
        {
            Success = false;
            Errors = errors ?? new List<string>();
            Message = errors?.Any() == true ? string.Join(", ", errors) : null;
            StatusCode = statusCode; // Set status code
        }
    }
}
