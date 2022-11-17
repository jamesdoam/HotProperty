using HotProperty_PropertyAPI.Data;
using HotProperty_PropertyAPI.Models;
using HotProperty_PropertyAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HotProperty_PropertyAPI.Repository
{
    public class PropertyRepository : Repository<Property>, IPropertyRepository
    {
        private readonly ApplicationDbContext _db;
        public PropertyRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public async Task<Property> UpdateAsync(Property entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.Properties.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
