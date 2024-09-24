using ProjectBlog.DAL.DB;
using ProjectBlog.DAL.Entities;

namespace ProjectBlog.DAL.Repositories
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(BlogContext db) : base(db)
        {
        }

        public new async Task Add(User user)
        {
            user.Roles.Add(new Role { Id = 1, RoleName = "Пользователь" });
            Set.Add(user);
            await _db.SaveChangesAsync();
        }
    }
}
