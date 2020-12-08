using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.DataTransferObjects.QueryDTOs;
using BusinessLayer.Facades.FacadeImplementations;
using BusinessLayer.Facades.FacadeInterfaces;
using BusinessLayer.QueryObjects;
using BusinessLayer.Services.Implementations;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer;
using DataAccessLayer.DataClasses;
using Infrastructure;
using Infrastructure.Query;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace API
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
            // autofac?
            
            services.AddScoped<IUserFacade, UserFacade>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUnitOfWorkProvider, UnitOfWorkProvider>();
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            
            // no idea if correct but works
            services.AddScoped<FilterDtoBase, UserFilterDto>();
            services.AddScoped<FilterDtoBase, TripFilterDto>();
            services.AddScoped<FilterDtoBase, ChallengeFilterDto>();
            services.AddScoped<FilterDtoBase, ReviewFilterDto>();
            services.AddScoped<FilterDtoBase, LocationFilterDto>();


            services.AddScoped(typeof(IQuery<User>), typeof(UserQuery));
            services.AddScoped(typeof(IQuery<Review>), typeof(ReviewQuery));
            services.AddScoped(typeof(IQuery<Challenge>), typeof(ChallengeQuery));
            services.AddScoped(typeof(IQuery<Trip>), typeof(TripQuery));
            services.AddScoped(typeof(IQuery<Location>), typeof(LocationQuery));
           // query obj base
           // problem with abstract?
           // below dont work
           //services.AddScoped<QueryObjectBase<User, UserDto, UserFilterDto, UserQuery>,typeof(UserQueryObject)>();
           //services.AddScoped(typeof(QueryObjectBase<User, UserDto, UserFilterDto, UserQuery>), typeof(UserQueryObject));
            
            services.AddScoped<DbContext, DatabaseContext>();
            services.AddSingleton(provider => 
                new Func<DbContext>(() => provider.GetService<DbContext>()));
            
            //services.AddDbContext<DatabaseContext>(options => options
            //    .UseNpgsql(@"Host=localhost;Database=tripdb;Username=postgres;Password=postgres;Port=5432"));
            services.AddApiVersioning(x =>
            {
                x.DefaultApiVersion = new ApiVersion(1, 0);
                x.AssumeDefaultVersionWhenUnspecified = true;
                x.ReportApiVersions = true;
            });
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
