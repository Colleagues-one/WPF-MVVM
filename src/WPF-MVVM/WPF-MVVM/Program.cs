using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace WPF_MVVM
{
    public static class Program 
    {
        [System.STAThreadAttribute()]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.0.0")]
        public static void Main()
        {
            WPF_MVVM.App app = new WPF_MVVM.App();
            app.InitializeComponent();
            app.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var host_builder = Host.CreateDefaultBuilder(args);

            host_builder.UseContentRoot(Environment.CurrentDirectory);
            host_builder.ConfigureAppConfiguration((host, cfg) =>
            {
                cfg.SetBasePath(Environment.CurrentDirectory);
                cfg.AddJsonFile("appsettings.json", optional: true, reloadOnChange:true);
            });

            host_builder.ConfigureServices(App.ConfigureServices);


            return host_builder;
        }
    }
}
