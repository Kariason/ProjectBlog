using System.ComponentModel.DataAnnotations;

namespace ProjectBlog.DAL.Entities
{
    public class AddArticleView
    {
        [Required(ErrorMessage = "Поле Заголовок должно быть заполнено")]
        [DataType(DataType.Text)]
        [Display(Name = "Название", Prompt = "Введите название статьи")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Поле Содержание должно быть заполнено")]
        [DataType(DataType.Text)]
        [StringLength(1000, ErrorMessage = "Статья не может быть пустой", MinimumLength = 3)]
        [Display(Name = "Текст", Prompt = "Введите текст статьи")]
        public string? Content { get; set; }

        public IList<Tag>? Tags { get; set; }
    }
}
