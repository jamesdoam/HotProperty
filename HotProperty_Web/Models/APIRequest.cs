using Microsoft.AspNetCore.Mvc;
using System.Net;
using static HotProperty_Utility.SD;

namespace HotProperty_Web.Models
{
    public class APIRequest
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string Url { get; set; } 
        public object Data { get; set; }
        public string Token { get; set; }
    }
}
