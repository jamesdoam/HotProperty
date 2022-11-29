using HotProperty_Web.Models;
using HotProperty_Web.Models.Dto;

namespace HotProperty_Web.Services.IServices
{
    public interface IPropertyService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(PropertyCreateDTO dto, string token);
        Task<T> UpdateAsync<T>(PropertyUpdateDTO dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);
    }
}
