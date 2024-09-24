using ProjectBlog.DAL.DB;
using ProjectBlog.DAL.Entities;
using System.Collections.Generic;

namespace ProjectBlog.DAL.Repositories
{
    public class ArticleRepository : Repository<Article>
    {
        public ArticleRepository(BlogContext db) : base(db)
        {
        }

        public IEnumerable<Article> GetArticlesByAuthorId(int user_Id)
        {
            var articles = Set.AsEnumerable().Where(x => x.UserId == user_Id);
            return articles.ToList();
        }
    }
}
