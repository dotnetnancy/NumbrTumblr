using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using VSNumberTumbler.Models;
using Microsoft.Extensions.Configuration;
using VSNumberTumbler.ViewModels;
using Newtonsoft.Json.Serialization;
using VSNumberTumbler.Models.Repositories;
using VSNumberTumbler.Domain;
using VSNumberTumbler.Services;

namespace VSNumberTumbler
{
    public class Startup
    {
        IHostingEnvironment _environment;
        //IConfigurationRoot _config;
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //if (_environment.IsEnvironment("Development") || _environment.IsEnvironment("Testing"))
            //{
            //    services.AddScoped<IWhateverService, WhateverService>();
            //}
            //else
            //{
            //    //implement a real whatever service vs the development or testing version
            //}
            //services.AddSingleton(_config);
            services.AddDbContext<NumberTumblerContext>();
            services.AddTransient<INumberTumblerRepository, NumberTumblerRepository>();
            services.AddTransient<INumberSetRepository, NumberSetRepository>();
            services.AddTransient<IShuffleRepository, ShuffleRepository>();
            services.AddTransient<IShuffleDomain, ShuffleDomain>();
            services.AddTransient<IShuffleService, ShuffleService>();
            services.AddTransient<INumberSetDomain, NumberSetDomain>();
            services.AddTransient<INumberSetService, NumberSetService>();
            services.AddTransient<NumberTumblerSeedData>();
            //special method to allow us to add serializer options for us we want to camelcase from pascalcase
            services.AddMvc()
                .AddJsonOptions(config =>
                {
                    config.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            IHostingEnvironment env, 
            ILoggerFactory loggerFactory,
            NumberTumblerSeedData seeder)
        {
            _environment = env;
            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<NumberSetViewModel, NumberSet>().ReverseMap();
                config.CreateMap<NumberSetNumberViewModel, NumberSetNumber>().ReverseMap();
                config.CreateMap<ShuffleViewModel, Shuffle>().ReverseMap();
                config.CreateMap<ShuffleNumberViewModel, ShuffleNumber>().ReverseMap();
            });

            var builder = new ConfigurationBuilder()
                .SetBasePath(_environment.ContentRootPath)
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();

            //_config = builder.Build();

            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=App}/{action=Index}/{id?}");
            });
            //we cannot make the Configure method async but the trick 
            //to make it run the async method is to use the Wait() vs 
            //if this was an async method and we would have called await seeder.EnsureSeedData();
            seeder.EnsureSeedData().Wait();
        }
    }
}
