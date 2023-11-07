using System.Net;
using static System.Net.WebRequestMethods;

namespace WPF_MVVM_Console
{
    internal class Program
    {
        private const string dataURL = @"https://github.com/CSSEGISandData/COVID-19/blob/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";

        static void Main(string[] args)
        {  
            HttpClient client = new HttpClient();
            var response = client.GetAsync(dataURL).Result;
            var csv_str = response.Content.ReadAsStringAsync().Result;

            
            Console.ReadKey();
        }
    }
}
