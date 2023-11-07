using System.Globalization;
using System.Net;
using System.Runtime.InteropServices.JavaScript;
using static System.Net.WebRequestMethods;

namespace WPF_MVVM_Console
{
    internal class Program
    {
        private const string dataURL = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";

        static void Main(string[] args)
        {
            
            /*foreach (var data_line in GetDataLines())
            {
                Console.WriteLine(data_line);
            }*/
            var dates = GetDates();
            Console.WriteLine(string.Join("\r\n", dates));
            
            Console.ReadKey();
        }


        private static async Task<Stream> GetDataStream()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(dataURL, HttpCompletionOption.ResponseHeadersRead);
            return await response.Content.ReadAsStreamAsync();
        }

        private static IEnumerable<string> GetDataLines()
        {
            using var data_stream = GetDataStream().Result;
            using var data_reader = new StreamReader(data_stream);
            while (!data_reader.EndOfStream)
            {
                var line = data_reader.ReadLine();
                if(string.IsNullOrWhiteSpace(line)) continue;
                yield return line;
            }
        }

        private static DateTime[] GetDates() => GetDataLines()
            .First()
            .Split(',')
            .Skip(4)
            .Select(s=>DateTime.Parse(s,CultureInfo.InvariantCulture))
            .ToArray();
    }
}
