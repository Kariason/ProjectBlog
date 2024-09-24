using ProjectBlog.DAL.DB;
using ProjectBlog.DAL.Entities;

namespace ProjectBlog.DAL.Repositories
{
    public class TagRepository : Repository<Tag>
    {
        public TagRepository(BlogContext db) : base(db)
        {
        }
    }
}
