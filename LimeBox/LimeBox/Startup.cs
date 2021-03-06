﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LimeBox.Models;
using LimeBox.Models.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LimeBox
{
    public class Startup
    {
        static public string connString;

        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            
            connString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<LimeContext>(o => o.UseSqlServer(connString));
            services.AddDbContext<IdentityDbContext>(o => o.UseSqlServer(connString));

            services.AddAuthentication(
                CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(o => o.LoginPath = "/Account/Login");
               

            services.AddIdentity<IdentityUser, IdentityRole>(o =>
            {
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
               
            })
           .AddEntityFrameworkStores<IdentityDbContext>()
           .AddDefaultTokenProviders();

            services.AddTransient<ShoppingCart>();
            services.AddTransient<IRepository, Repository>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
