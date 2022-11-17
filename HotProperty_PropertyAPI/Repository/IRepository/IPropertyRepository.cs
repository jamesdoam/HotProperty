using HotProperty_PropertyAPI.Models;
using System.Linq.Expressions;

namespace HotProperty_PropertyAPI.Repository.IRepository
{
    public interface IPropertyRepository : IRepository<Property>
    {
        Task<Property> UpdateAsync(Property entity);

    }
}
