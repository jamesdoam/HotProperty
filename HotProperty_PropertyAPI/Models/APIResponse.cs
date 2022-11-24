using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Net;

namespace HotProperty_PropertyAPI.Models
{
    public class APIResponse
    {
        public APIResponse()
        {
            ErrorMessage = new List<String>();
        }
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<String> ErrorMessage { get; set; }
        public object Result {get;set;}
    }
}
