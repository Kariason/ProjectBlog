using ProjectBlog.DAL.DB;
using ProjectBlog.DAL.Entities;

namespace ProjectBlog.DAL.Repositories
{
    public class RoleRepository : Repository<Role>
    {
        public RoleRepository(BlogContext db) : base(db)
        {
        }
    }
}
