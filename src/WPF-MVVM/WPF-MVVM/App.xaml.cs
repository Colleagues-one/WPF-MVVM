using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WPF_MVVM.Services;
using WPF_MVVM.ViewModels;

namespace WPF_MVVM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static bool IsDesignMode = true;

        protected override void OnStartup(StartupEventArgs e)
        {
            IsDesignMode = false;

            base.OnStartup(e);
            /*var service_test = new DataService();
            var countries = service_test.GetData().ToArray();*/
        }




        public static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services.AddSingleton<DataService>();
            services.AddSingleton<CountriesStatisticViewModel>();

        }
    }
}
