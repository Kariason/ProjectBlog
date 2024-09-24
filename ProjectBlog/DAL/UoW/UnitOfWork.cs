using ProjectBlog.DAL.DB;
using ProjectBlog.DAL.Repositories;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ProjectBlog.DAL.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private BlogContext _appContext;

        private Dictionary<Type, object>? _repositories;

        public UnitOfWork(BlogContext app)
        {
            this._appContext = app;
        }

        public void Dispose()
        {
        }

        public IRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = true) where TEntity : class
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<Type, object>();
            }

            if (hasCustomRepository)
            {
                var customRepo = _appContext.GetService<IRepository<TEntity>>();
                if (customRepo != null)
                {
                    return customRepo;
                }
            }

            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type))
            {
                _repositories[type] = new Repository<TEntity>(_appContext);
            }

            return (IRepository<TEntity>)_repositories[type];

        }
        public int SaveChanges(bool ensureAutoHistory = false)
        {
            throw new NotImplementedException();
        }
    }
}
