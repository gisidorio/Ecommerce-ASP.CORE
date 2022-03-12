using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CasaDoCodigo.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CasaDoCodigo
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
            // SERVE PARA ADICIONAR SERVIÇOS À SUA APLICAÇÃO. PODE SER DE BANCO DE DADOS LOG MONITORAMENTO, MVC E ETC
            services.AddMvc();

            /* CONNECTION STRING RECEBE A STRING DE CONEXÃO QUE ESTÁ NO 
             * ARQUIVO DE CONFIGURAÇÃO appsettings.json */
            string connectionString = Configuration.GetConnectionString("Default");

            // ADICIONAR CONTEXTO DO BANCO DE DADOS
            services.AddDbContext<ApplicationContext>(options => 
                options.UseSqlServer(connectionString));

            services.AddTransient<IDataService, DataService>();
            services.AddTransient<IProdutoRepository, ProdutoRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // LOCAL ONDE VOCÊ VAI UTILIZAR/consumir OS SERVIÇOS QUE FORAM ADICIONADOS ACIMA NO CONFIGURE SERVICES
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, 
            IServiceProvider serviceProvider)
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

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Pedido}/{action=Carrossel}/{id?}");
            });

            // CHAMADA DE MÉTODO PARA GARANTIR QUE A BASE DE DADOS FOI CRIADA
            serviceProvider.GetService<IDataService>().InicializaDB();
        }
    }
}
