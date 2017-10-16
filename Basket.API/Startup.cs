namespace BasketService.API
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using Akka.Actor;

    using BasketService.Actors.Baskets;
    using BasketService.Actors.Products;

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
            services.AddMvcCore()
               .AddApiExplorer()
               .AddJsonFormatters();

            services.AddSwaggerGen(s => {
                s.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "Basket Api", Version = "v1" });
            });

            services.AddSingleton(_ => ActorSystem.Create("basketservice"));

            services.AddSingleton<BasketsActorProvider>();
            services.AddSingleton<ProductsActorProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseSwagger(s => {
                s.RouteTemplate = "help/documentation/{documentName}/basketapi.json";
            });
            app.UseSwaggerUI(s => {
                s.RoutePrefix = "help/documentation";
                s.SwaggerEndpoint("/help/documentation/v1/basketapi.json", "Basket Api");
            });
        }
    }
}
