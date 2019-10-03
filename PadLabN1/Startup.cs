using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Configuration;
using PadLabN1.Entities;
using PadLabN1.Services;
using System.Reflection;
using PadLabN1.Controllers;
using PadLabN1.Hubs;
using PadLabN1.Models;

namespace PadLabN1
{
    public class Startup
    {
        public Startup( IConfiguration configuration )
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }


        public void ConfigureServices( IServiceCollection services )
        {
            services.AddMvc( ).SetCompatibilityVersion( CompatibilityVersion.Version_2_2 );
            services.AddTransient<IDataManager, DbDataManager>( );
            services.AddTransient<MessageController>( );
            services.AddCors( options =>
                options.AddPolicy( "Allow any", x =>
                {
                    x.WithOrigins( "http://localhost:4200" );
                    // .AllowAnyOrigin()
                    x.AllowAnyMethod( );
                    x.AllowAnyHeader( );
                    x.AllowCredentials( );
                } )
            );
            services.AddSignalR( );


            //services.AddDbContext<PadLabN1DbContext>(o => o.UseSqlServer(connectionString));

            services.AddDbContext<PadLabN1DbContext>( options =>
            {
                options.UseMySQL( Configuration["ConnectionStrings:dbURL"] );
            } );
        }


        public void Configure( IApplicationBuilder app, IHostingEnvironment env )
        {
            if ( env.IsDevelopment( ) )
            {
                app.UseDeveloperExceptionPage( );
            }
            else
            {
                app.UseHsts( );
            }

            app.UseCors( "Allow any" );
            app.UseHttpsRedirection( );
            app.UseStatusCodePages( );
            app.UseMvc( );

            app.UseSignalR( routes => { routes.MapHub<MessageHub>( "/message" ); } );

            AutoMapper.Mapper.Initialize( cfg =>
            {
                cfg.CreateMap<User, UserDto>( );
                cfg.CreateMap<Post, PostDto>( )
                    .ForMember( d => d.Name,
                        opts => opts.MapFrom( p => p.User != null ? p.User.Name : "Unknown" ) );
                cfg.CreateMap<User, UserForCreation>( );
                cfg.CreateMap<Post, PostForCreation>( );
                cfg.CreateMap<Sub, SubDto>( );
                cfg.CreateMap<SubForCreation, Sub>( );
                cfg.CreateMap<User, UserWithSubsDto>( );
            } );
        }
    }
}