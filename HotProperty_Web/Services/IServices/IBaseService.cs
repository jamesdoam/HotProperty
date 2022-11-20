using HotProperty_Web.Models;

namespace HotProperty_Web.Services.IServices
{
    public interface IBaseService
    {
        APIResponse reponseModel { get; set; }
        Task<T> SendAsync<T>(APIRequest apiRequest);
    }
}
