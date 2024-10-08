﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectBlog.DAL.Entities
{
    public class Article
    {
        public int Id { get; set; }
        public DateTimeOffset ArticleDate { get; set; }

        [Required(ErrorMessage = "Поле Заголовок должно быть заполнено")]
        [DataType(DataType.Text)]
        [Display(Name = "Название", Prompt = "Введите название статьи")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Поле Содержание должно быть заполнено")]
        [DataType(DataType.Text)]
        [StringLength(1000, ErrorMessage = "Статья не может быть пустой.", MinimumLength = 3)]
        [Display(Name = "Текст", Prompt = "Введите текст статьи")]
        public string? Content { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("ArticleId")]
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
