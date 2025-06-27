namespace MyMiniBank.Api.Models.DTOs
{
    /// <summary>
    /// Represents a standardized API response structure.
    /// </summary>
    public class ApiResponse<T>
    {
        public bool Success { get; set; } // Indicates if the API call was successful
        public T? Data { get; set; } // The data returned by the API, can be null if not applicable
        public string? Message { get; set; } // A message providing additional context or information about the response
        public int StatusCode { get; set; } = 200; // The HTTP status code of the response

        // Constructor to initialize the ApiResponse with default values
        public static ApiResponse<T> Ok(T data, string? message = null) => new()
        {
            Success = true,
            Data = data,
            Message = message ?? "Success",
            StatusCode = 200 // Default status code for a successful response
        };
        public static ApiResponse<T> Fail(string message, int statusCode=400) => new()
        {
            Success = false,
            Data = default,
            Message = message,
            StatusCode = statusCode // Default status code for a failed response
        };
    }
}