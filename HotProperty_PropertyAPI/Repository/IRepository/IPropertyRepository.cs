using HotProperty_PropertyAPI.Models;
using System.Linq.Expressions;

namespace HotProperty_PropertyAPI.Repository.IRepository
{
    public interface IPropertyRepository
    {
        Task<List<Property>> GetAllAsync(Expression<Func<Property, bool>> filter = null);
        Task<Property> GetAsync(Expression<Func<Property, bool>> filter = null, bool tracked = true);
        Task CreateAsync(Property entity);   
        Task RemoveAsync(Property entity);
        Task UpdateAsync(Property entity);
        Task SaveAsync();
    }
}
