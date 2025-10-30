using Backend.Utility.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Utility
{
    public static class ApiResponseHelper
    {
        public static IActionResult Success<T>(T result, string? message = null)
        {
            return new OkObjectResult(new ApiResponse<T>
            {
                Status = true,
                Message = message,
                Result = result
            });
        }
           

        public static IActionResult Fail(string message)
        {
            return new BadRequestObjectResult(new ApiResponse<Object>
            {
                Status = false,
                Message = message,
                Result = default
            });
        }
    }
}
