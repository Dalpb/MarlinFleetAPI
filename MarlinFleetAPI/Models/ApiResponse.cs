using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarlinFleetAPI.Models
{
    public class ApiResponse<T>
    {
        private string message { get; set; }
        private T data { get; set; }
        private bool success { get; set; }

        public ApiResponse(string message, T data, bool success)
        {
            this.message = message;
            this.data = data;
            this.success = success;
        }

        public static ApiResponse<object> SystemErrorResponse()
        {
            return new ApiResponse<object>("System Error",null,false);
        }
    }
}