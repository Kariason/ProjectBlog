using System.ComponentModel.DataAnnotations;

namespace ProjectBlog.DAL.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле Имя должно быть заполнено")]
        [DataType(DataType.Text)]
        [Display(Name = "Имя", Prompt = "Введите имя")]
        [StringLength(100)]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Поле Фамилия должно быть заполнено")]
        [DataType(DataType.Text)]
        [Display(Name = "Фамилия", Prompt = "Введите фамилию")]
        [StringLength(100)]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Поле Email должно быть заполнено")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email", Prompt = "Ваш логин")]
        [StringLength(100)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Поле Пароль должно быть заполнено")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль", Prompt = "Введите пароль")]
        [StringLength(50, ErrorMessage = "Хотя бы 3 символа, пожалуйста")]
        public string? Password { get; set; }

        public int RoleId { get; set; } = 1;

        public List<Role> Roles { get; set; } = new List<Role>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
