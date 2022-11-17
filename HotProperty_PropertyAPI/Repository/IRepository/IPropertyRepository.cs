using HotProperty_PropertyAPI.Models;
using System.Linq.Expressions;

namespace HotProperty_PropertyAPI.Repository.IRepository
{
    public interface IPropertyRepository
    {
        Task<List<Property>> GetAll(Expression<Func<Property, bool>> filter = null);
        Task<Property> Get(Expression<Func<Property, bool>> filter = null, bool tracked = true);
        Task Create(Property entity);   
        Task Remove(Property entity);
        Task Save();
    }
}
