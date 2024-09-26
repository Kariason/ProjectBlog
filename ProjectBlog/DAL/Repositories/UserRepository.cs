using Microsoft.EntityFrameworkCore;
using ProjectBlog.DAL.DB;
using ProjectBlog.DAL.Entities;

namespace ProjectBlog.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        protected DbContext _db;

        public DbSet<User> Set { get; private set; }

        public UserRepository(BlogContext db)
        {
            _db = db;
            var set = _db.Set<User>();
            set.Load();
            Set = set;
        }

        public async Task Add(User item)
        {
            Set.Add(item);
            await _db.SaveChangesAsync();
        }

        public async Task<User> Get(int id)
        {
            return await Set.FindAsync(id);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await Set.ToListAsync();
        }

        public User GetByLogin(string login)
        {
           
            return Set.FirstOrDefault(x => x.Email == login);
        }

        public async Task Delete(User item)
        {
            Set.Remove(item);
            await _db.SaveChangesAsync();
        }

        public async Task Update(User item)
        {
            var existingItem = await Set.FindAsync(GetKeyValue(item));

            if (existingItem != null)
            {
                _db.Entry(existingItem).CurrentValues.SetValues(item);
                await _db.SaveChangesAsync();
            }
        }

        private object GetKeyValue(User item)
        {
            var key = _db.Model.FindEntityType(typeof(User)).FindPrimaryKey().Properties.FirstOrDefault();
            return item.GetType().GetProperty(key.Name).GetValue(item);
        }
    }
}
