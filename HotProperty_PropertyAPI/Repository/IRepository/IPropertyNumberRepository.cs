using HotProperty_PropertyAPI.Models;
using System.Linq.Expressions;

namespace HotProperty_PropertyAPI.Repository.IRepository
{
    public interface IPropertyNumberRepository : IRepository<PropertyNumber>
    {
        Task<PropertyNumber> UpdateAsync(PropertyNumber entity);
    }
}
