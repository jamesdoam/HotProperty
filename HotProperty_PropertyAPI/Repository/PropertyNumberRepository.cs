using HotProperty_PropertyAPI.Data;
using HotProperty_PropertyAPI.Models;
using HotProperty_PropertyAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HotProperty_PropertyAPI.Repository
{
    public class PropertyNumberRepository : Repository<PropertyNumber>, IPropertyNumberRepository
    {
        private readonly ApplicationDbContext _db;
        public PropertyNumberRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public async Task<PropertyNumber> UpdateAsync(PropertyNumber entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.PropertyNumbers.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
