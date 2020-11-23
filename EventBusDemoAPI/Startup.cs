using Autofac;
using Autofac.Extensions.DependencyInjection;

using EventBus.RabbitMQ;

using EventBusDemoAPI.Events;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

using Pluto.EventBus;
using Pluto.EventBus.Abstractions;

using RabbitMQ.Client;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventBusDemoAPI
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EventBusDemoAPI", Version = "v1" });
            });

            // ע�� mq���Ӷ���
            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();
                var factory = new ConnectionFactory()
                {
                    HostName = "127.0.0.1",
                    DispatchConsumersAsync = true
                };
                factory.UserName ="guest";
                factory.Password = "guest";
                var retryCount = 5;
                return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
            });

            // ע���ڴ涩�Ĺ���
            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

            // ע��mq�¼�����
            services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
            {
                var queryName = "EventBusDemoAPI";
                var rabbitMqPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                var retryCount = 5;
                return new EventBusRabbitMQ(
                    rabbitMqPersistentConnection, 
                    logger, iLifetimeScope, 
                    eventBusSubcriptionsManager, 
                    queryName, 
                    retryCount);
            });

            // ע���¼��������
            services.AddTransient<DemoEventHandler>();

        }


        /// <summary>
        /// ���õ�����(autofac)����
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // ע�ᶩ����
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<DemoEvent, DemoEventHandler>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EventBusDemoAPI v1"));
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
