using HotProperty_Web.Models;
using HotProperty_Web.Models.Dto;

namespace HotProperty_Web.Services.IServices
{
    public interface IPropertyService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(PropertyCreateDTO dto);
        Task<T> UpdateAsync<T>(PropertyUpdateDTO dto);
        Task<T> DeleteAsync<T>(int id);
    }
}
