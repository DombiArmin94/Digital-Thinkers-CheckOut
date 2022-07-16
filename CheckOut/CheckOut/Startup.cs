using Checkout.Service;
using Checkout.Repository;

namespace BTS.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            Configuration = configuration;
            this.logger = logger;
        }

        public IConfiguration Configuration { get; }
        private ILogger<Startup> logger;



        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IMoneyStockRepository, MoneyStockRepository>();

            services.AddScoped<IMoneyStockService, MoneyStockService>();
            services.AddScoped<ICurrencyConverterAPIService, CurrencyConverterAPIService>();

            services.AddMvc(options => options.EnableEndpointRouting = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
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
                    template: "{controller}/{action}/{id?}");

                routes.MapRoute(
                    "NotFound",
                    "{*.}",
                    new { controller = "Home", action = "Error" }
                );
            });
        }
    }
}