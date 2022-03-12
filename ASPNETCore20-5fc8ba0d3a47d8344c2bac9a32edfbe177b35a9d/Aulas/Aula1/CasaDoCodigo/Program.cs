using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CasaDoCodigo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // quanddo rodamos a aplicação é feito A CHAMADA PARA Ó MÉTODO
            BuildWebHost(args).Run();
        }

        // MÉTODO QUE FAZ A CONSTRUÇÃO NA HOSPEDAGEM WEB
        // O ARQUIVO STARTUP CONTEM CONFIRMAÇÕES DE CONFIGURAÇÃO DA APLICAÇÃO
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
