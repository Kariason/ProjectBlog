using Microsoft.EntityFrameworkCore;
using ProjectBlog.DAL.DB;
using ProjectBlog.DAL.Entities;

namespace ProjectBlog.DAL.Repositories
{
    public class RoleRepository : IRepository<Role>
    {
        protected DbContext _db;

        public DbSet<Role> Set { get; private set; }

        public RoleRepository(BlogContext db)
        {
            _db = db;
            var set = _db.Set<Role>();
            set.Load();
            Set = set;
        }

        public async Task Add(Role item)
        {
            Set.Add(item);
            await _db.SaveChangesAsync();
        }

        public async Task<Role> Get(int id)
        {
            return await Set.FindAsync(id);
        }

        public async Task<IEnumerable<Role>> GetAll()
        {
            return await Set.ToListAsync();
        }

        public async Task Delete(Role item)
        {
            Set.Remove(item);
            await _db.SaveChangesAsync();
        }

        public async Task Update(Role item)
        {
            var existingItem = await Set.FindAsync(GetKeyValue(item));

            if (existingItem != null)
            {
                _db.Entry(existingItem).CurrentValues.SetValues(item);
                await _db.SaveChangesAsync();
            }
        }

        private object GetKeyValue(Role item)
        {
            var key = _db.Model.FindEntityType(typeof(Role)).FindPrimaryKey().Properties.FirstOrDefault();
            return item.GetType().GetProperty(key.Name).GetValue(item);
        }
    }
}
