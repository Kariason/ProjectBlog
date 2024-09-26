using Microsoft.EntityFrameworkCore;
using ProjectBlog.DAL.DB;
using ProjectBlog.DAL.Entities;
using System.Collections.Generic;

namespace ProjectBlog.DAL.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        protected DbContext _db;

        public DbSet<Article> Set { get; private set; }

        public ArticleRepository(BlogContext db)
        {
            _db = db;
            var set = _db.Set<Article>();
            set.Load();
            Set = set;
        }

        public async Task Add(Article item)
        {
            Set.Add(item);
            await _db.SaveChangesAsync();
        }

        public async Task<Article> Get(int id)
        {
            return await Set.Include(a => a.Tags).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Article>> GetAll()
        {
            return await Set.Include(a => a.Tags).ToListAsync();
        }

        public IEnumerable<Article> GetArticlesByAuthorId(int userId)
        {
            var articles = Set.Include(a => a.Tags).AsEnumerable().Where(x => x.UserId == userId);
            return articles.ToList();
        }

        public async Task Delete(Article item)
        {
            Set.Remove(item);
            await _db.SaveChangesAsync();
        }

        public async Task Update(Article item)
        {
            //var existingItem = await Set.FindAsync(GetKeyValue(item));
            var existingItem = await Set.Include(a => a.Tags).FirstOrDefaultAsync(a => a.Id == item.Id);

            if (existingItem != null)
            {
                _db.Entry(existingItem).CurrentValues.SetValues(item);

                existingItem.Tags.Clear();

                foreach (var tag in item.Tags)
                {
                    var existingTag = await _db.Set<Tag>().FirstOrDefaultAsync(t => t.Id == tag.Id);

                    if (existingTag != null)
                    {
                        existingItem.Tags.Add(existingTag);
                    }
                    else
                    {
                        existingItem.Tags.Add(tag);
                    }
                }

                await _db.SaveChangesAsync();
            }
        }

        private object GetKeyValue(Article item)
        {
            var key = _db.Model.FindEntityType(typeof(Article)).FindPrimaryKey().Properties.FirstOrDefault();
            return item.GetType().GetProperty(key.Name).GetValue(item);
        }
    }
}
