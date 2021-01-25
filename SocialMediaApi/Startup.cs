using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SocialMediaCore.CustomEntities;
using SocialMediaCore.Interfaces;
using SocialMediaCore.Services;
using SocialMediaInfrastructure.Data;
using SocialMediaInfrastructure.Filters;
using SocialMediaInfrastructure.Interfaces;
using SocialMediaInfrastructure.Repositories;
using SocialMediaInfrastructure.Services;
using System;
using System.IO;
using System.Reflection;

namespace SocialMediaApi
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
            //Añadir automapping
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddControllers(options=> {
                options.Filters.Add<GlobalExceptionFilter>();
            }).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore; 
            })
            .ConfigureApiBehaviorOptions(options =>
            {
                //options.SuppressModelStateInvalidFilter = true;
            });

            services.Configure<PaginationOption>(Configuration.GetSection("Pagination"));
            //Aquí configurar servicios para las dependencias. Similar a angular
            //cuando se use esa abstraccion se usará esa implementación
            services.AddDbContext<SocialMediaContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SocialMedia"))
            );
            services.AddTransient<IPostService, PostService>();
            //services.AddTransient<IUserRepository, UserRepository>();
            //services.AddTransient<IPostRepository, PostRepository>();//Para inyectar el repositorio adecuado a la herramienta
            //Para usar el filter como middleware, es decir un filtro de forma global
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));//La diferencia con transient es el ciclo de vida del objeto
            services.AddTransient<IUnitWork, UnitOfWork>();
            services.AddSingleton<IUriService>(provider => {
                var accesor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accesor.HttpContext.Request;
                var absoluteUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(absoluteUri);

            });
            services.AddSwaggerGen(doc =>
            {
                doc.SwaggerDoc("v1", new OpenApiInfo { Title="Social Media API", Version="v1"});
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                doc.IncludeXmlComments(xmlPath);
            });
            services.AddMvc(options =>
            {
                options.Filters.Add<ValidationFilter>();
            }).AddFluentValidation(options=> {
                //PostValidator
                options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(options=>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json","Social Media API");
                options.RoutePrefix = string.Empty;
            });
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
