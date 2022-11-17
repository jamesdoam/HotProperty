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

        public async Task Create(Property entity)
        {
            await _db.Properties.AddAsync(entity);
            await Save();
        }

        public async Task<Property> Get(Expression<Func<Property, bool>> filter = null, bool tracked = true)
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

        public async Task<List<Property>> GetAll(Expression<Func<Property, bool>> filter = null)
        {
            IQueryable<Property> query = _db.Properties;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

        public async Task Remove(Property entity)
        {
            _db.Properties.Remove(entity);
            await Save();
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
