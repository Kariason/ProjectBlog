using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using ProjectBlog.DAL.DB;
using ProjectBlog.DAL.Entities;
using ProjectBlog.DAL.Repositories;
using ProjectBlog.Middlewares;
using ProjectBlog.Extentions;


namespace ProjectBlog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

            // Добавление БД
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services
                .AddDbContext<BlogContext>(options => options.UseNpgsql(connectionString))

                .AddUnitOfWork()
                    .AddCustomRepository<Comment, CommentRepository>()
                    .AddCustomRepository<Tag, TagRepository>()
                    .AddCustomRepository<Role, RoleRepository>();

            builder.Services
                .AddTransient<IUserRepository, UserRepository>()
                .AddTransient<IArticleRepository, ArticleRepository>();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).
                AddCookie(options =>
                {
                    options.LoginPath = new PathString("/Authenticate");
                });

            // Добавление сервисов в контейнер
            builder.Services.AddControllersWithViews();

            // NLog: Настройка NLog для внедрения зависимостей
            builder.Logging.ClearProviders();
           

            var app = builder.Build();

            // Настройте конвейера HTTP-запросов.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();


            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}

