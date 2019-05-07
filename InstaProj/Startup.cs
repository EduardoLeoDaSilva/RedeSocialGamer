using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstaProj.BancoDados;
using InstaProj.Controllers.ControllersTempoReal;
using InstaProj.Identity;
using InstaProj.Models.Entidades;
using InstaProj.Models.Identity;
using InstaProj.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using TableDependency.SqlClient;

namespace InstaProj
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<IdentityContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Identity")));
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));

            services.AddIdentity<UsuarioIdentity, IdentityRole>().AddEntityFrameworkStores<IdentityContext>();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Principal/index";
                options.AccessDeniedPath = "/Principal/index";
                options.SlidingExpiration = true;
            });

            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IAutenticaoRepository, AutenticaoRepository>();
            services.AddTransient<INoticiasRepository, NoticiasRepository>();
            services.AddTransient<IPostagenRepository, PostagenRepository>();
            services.AddTransient<ILikeRepository, LikeRepository>();
            services.AddTransient<IImagemRepository, ImagemRepository>();
            services.AddTransient<IComentarioRepository, ComentarioRepository>();
            services.AddTransient<IUsuarioLogadoRepository, UsuarioLogadoRepository>();
            services.AddTransient<IAmigoRepository, AmigoRepository>();
            services.AddSingleton<NotificacaoDbPostagens, NotificacaoDbPostagens>();
            services.AddTransient<SqlTableDependency<Postagem>>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            services.AddSignalR(o => {
            }).AddJsonProtocol();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();
            app.UseSignalR(routes =>
            {
            routes.MapHub<PostagemHub>("/postagemHub");
            });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Principal}/{action=index}/{id?}");

            });


        }
    }


}


