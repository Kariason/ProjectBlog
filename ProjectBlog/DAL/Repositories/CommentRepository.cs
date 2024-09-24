using ProjectBlog.DAL.DB;
using ProjectBlog.DAL.Entities;

namespace ProjectBlog.DAL.Repositories
{
    public class CommentRepository : Repository<Comment>
    {
        public CommentRepository(BlogContext db) : base(db)
        {
        }
    }
}
