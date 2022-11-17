using HotProperty_PropertyAPI.Data;
using HotProperty_PropertyAPI.Models;
using HotProperty_PropertyAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HotProperty_PropertyAPI.Repository
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly ApplicationDbContext _db;
        public PropertyRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(Property entity)
        {
            await _db.Properties.AddAsync(entity);
            await SaveAsync();
        }

        public async Task<Property> GetAsync(Expression<Func<Property, bool>> filter = null, bool tracked = true)
        {
            IQueryable<Property> query = _db.Properties;
            if(!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<Property>> GetAllAsync(Expression<Func<Property, bool>> filter = null)
        {
            IQueryable<Property> query = _db.Properties;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

        public async Task RemoveAsync(Property entity)
        {
            _db.Properties.Remove(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Property entity)
        {
            _db.Properties.Update(entity);
            await SaveAsync();
        }
    }
}
