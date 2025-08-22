using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarlinFleetAPI.Models
{
    //buena praactica según montoya ,fe
    public class ApiResponse<T>
    {
        public string message { get; set; }
        public T data { get; set; }
        public bool success { get; set; }

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