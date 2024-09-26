using ProjectBlog.DAL.Entities;

namespace ProjectBlog.DAL.Repositories
{
    public interface IUserRepository
    {
        Task Add(User item);
        Task<User> Get(int id);
        Task<IEnumerable<User>> GetAll();
        User GetByLogin(string login);
        Task Update(User item);
        Task Delete(User item);
    }
}
