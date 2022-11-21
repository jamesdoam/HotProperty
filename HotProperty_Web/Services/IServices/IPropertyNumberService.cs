using HotProperty_Web.Models;
using HotProperty_Web.Models.Dto;

namespace HotProperty_Web.Services.IServices
{
    public interface IPropertyNumberService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(PropertyNumberCreateDTO dto);
        Task<T> UpdateAsync<T>(PropertyNumberUpdateDTO dto);
        Task<T> DeleteAsync<T>(int id);
    }
}
