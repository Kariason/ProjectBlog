using ProjectBlog.DAL.DB;
using ProjectBlog.DAL.Entities;
using ProjectBlog.DAL.Repositories;
using ProjectBlog.Extentions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;


internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add DB
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


        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProjectBlogAPI", Version = "v1" }); });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProjectBlogAPI v1"));
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
