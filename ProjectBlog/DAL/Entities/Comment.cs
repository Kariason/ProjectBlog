namespace ProjectBlog.DAL.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public DateTime CommentDate { get; set; }
        public string? Content { get; set; }
        public int? UserId { get; set; }

        public User User { get; set; }
        public int? ArticleId { get; set; }
        public Article Article { get; set; }
    }
}
