namespace ProjectBlog.DAL.Entities
{
    public class Tag
    {
        public int Id { get; set; }
        public string? Content { get; set; }

        public ICollection<Article> Articles { get; set; } = new List<Article>();
    }
}
