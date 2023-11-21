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

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
           /* var service_test = new DataService();
            var countries = service_test.GetData().ToArray();*/
        }
    }

 

}
