using System.Configuration;
using System.Data;
using System.Windows;
using WPF_MVVM.Services;

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
    }

 

}
