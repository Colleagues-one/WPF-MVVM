using System.Globalization;
using System.Net;
using System.Runtime.InteropServices.JavaScript;
using static System.Net.WebRequestMethods;

namespace WPF_MVVM_Console
{
    internal class Program
    {
        private const string dataURL =
            @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";

        static void Main(string[] args)
        {
            string str = "lat:-14.3559,lon:-489";

            string[] comp = str.Split(',');

            double lat = double.Parse(comp[0].Split(':')[1], CultureInfo.InvariantCulture);
            double lon = double.Parse(comp[1].Split(':')[1], CultureInfo.InvariantCulture);


            
            
            /*var russia = GetData().First(v => v.Country.Equals("Russia", StringComparison.OrdinalIgnoreCase));
            Console.WriteLine(string.Join("\r\n", GetDates().Zip(russia.Counts,(date,count) => $"{date:dd.MM.yyyy} - {count}")));
             Console.ReadKey();*/
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
                if (string.IsNullOrWhiteSpace(line)) continue;
                yield return line.Replace("Korea,", "Korea-");
            }
        }

        private static DateTime[] GetDates() => GetDataLines()
            .First()
            .Split(',')
            .Skip(4)
            .Select(s => DateTime.Parse(s, CultureInfo.InvariantCulture))
            .ToArray();

        private static IEnumerable<(string Country, string Province, int[] Counts)> GetData()
        {
            
            var lines = GetDataLines()
                .Skip(1)
                .Select(line => line.Split(','));
            foreach (var row in lines)
            {
                var province = row[0].Trim();
                var country_name = row[1].Trim(' ', '"');
                var counts = row.Skip(5)
                    .Select(int.Parse).ToArray();
                yield return(country_name, province, counts);
            }
        }
    }
}



