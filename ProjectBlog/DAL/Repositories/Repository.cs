using Microsoft.EntityFrameworkCore;
using ProjectBlog.DAL.DB;
using ProjectBlog.DAL.Entities;

namespace ProjectBlog.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected DbContext _db;

        public DbSet<T> Set { get; private set; }

        public Repository(BlogContext db)
        {
            _db = db;
            var set = _db.Set<T>();
            set.Load();
            Set = set;
        }

        public async Task Add(T item)
        {
            Set.Add(item);
            await _db.SaveChangesAsync();
        }

        public async Task<T> Get(int id)
        {
            return await Set.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await Set.ToListAsync();
        }

        public User GetByLogin(string login)
        {
            return Set.FirstOrDefault(x => (x as User).Email == login) as User;
        }

        public async Task Delete(T item)
        {
            Set.Remove(item);
            await _db.SaveChangesAsync();
        }

        public async Task Update(T item)
        {
            Set.Update(item);
            await _db.SaveChangesAsync();
        }

        public async Task Update(T item, T newItem)
        {
            item = newItem;
            Set.Update(item);
            await _db.SaveChangesAsync();
        }
    }
}
