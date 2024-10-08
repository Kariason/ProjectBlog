using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using ProjectBlog.DAL.DB;
using ProjectBlog.DAL.Entities;
using ProjectBlog.DAL.Repositories;
using ProjectBlog.Middlewares;
using ProjectBlog.Extentions;
using NLog;
using NLog.Web;



namespace ProjectBlog
{
    public class Program
    {
        public static void Main(string[] args)
        {

            // Early init of NLog to allow startup and exception logging, before host is built
            var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
            logger.Debug("init main");

            try
            {
                var builder = WebApplication.CreateBuilder(args);
                builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();


                // Add DB
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                builder.Services
                    .AddDbContext<BlogContext>(options => options.UseNpgsql(connectionString))

                    .AddUnitOfWork()
                        //.AddCustomRepository<User, UserRepository>()
                        //.AddCustomRepository<Article, ArticleRepository>()
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

                // Add services to the container.
                builder.Services.AddControllersWithViews();

                // NLog: Setup NLog for Dependency injection
                builder.Logging.ClearProviders();
                //builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                //LogManager.Configuration = new XmlLoggingConfiguration("NLog.config");
                builder.Host.UseNLog();

                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (!app.Environment.IsDevelopment())
                {
                    app.UseExceptionHandler("/Home/Error");
                    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                    app.UseHsts();
                }

                app.UseHttpsRedirection();
                app.UseStaticFiles();

                app.UseMiddleware<ExceptionHandlingMiddleware>();

                app.UseRouting();

                app.UseAuthentication();
                app.UseAuthorization();

                app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                app.Run();

            }
            catch (Exception exception)
            {
                // NLog: catch setup errors
                logger.Error(exception, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
        }


    }
}

