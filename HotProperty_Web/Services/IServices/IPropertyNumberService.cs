using HotProperty_Web.Models;
using HotProperty_Web.Models.Dto;

namespace HotProperty_Web.Services.IServices
{
    public interface IPropertyNumberService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id,string token);
        Task<T> CreateAsync<T>(PropertyNumberCreateDTO dto, string token);
        Task<T> UpdateAsync<T>(PropertyNumberUpdateDTO dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);
    }
}
