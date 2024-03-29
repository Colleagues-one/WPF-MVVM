﻿using System.Configuration;
using System.Data;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WPF_MVVM.Services;
using WPF_MVVM.Services.Interfaces;
using WPF_MVVM.ViewModels;

namespace WPF_MVVM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static bool IsDesignMode = true;

        protected override async void OnStartup(StartupEventArgs e)
        {
            IsDesignMode = false;

            var host = Host;

            base.OnStartup(e);

            await host.RunAsync().ConfigureAwait(false);
            /*var service_test = new DataService();
            var countries = service_test.GetData().ToArray();*/
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            var host = Host;
            await host.StopAsync().ConfigureAwait(false);
            host.Dispose();
            _Host = null;
        }


        private static IHost _Host;

        public static IHost Host => _Host ??= Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

        public static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            //перенесено в serviceRegistration
            //services.AddSingleton<IDataService, DataService>();
            //services.AddTransient<IDataService, DataService>(); создается когда требуется

           /* services.AddScoped<IDataService, DataService>(); создание области для обработки сервиса
            using (var scope = App.Host.Services.CreateScope())
            {
                var data = scope.ServiceProvider.GetRequiredService<IDataService>();
            }*/
                
            //перенесено в 
            //services.AddSingleton<MainWindowViewModel>();//единичное создание
            //services.AddSingleton<CountriesStatisticViewModel>();


            services.RegisterServices().RegisterViewModels();

        }

        public static string CurrentDirectory => IsDesignMode ? Path.GetDirectoryName(GetSourceCodePath()) : Environment.CurrentDirectory;

        private static string GetSourceCodePath([CallerFilePath] string path = null) => path;
    }
}
