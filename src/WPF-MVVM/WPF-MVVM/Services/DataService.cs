using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_MVVM.Models;

namespace WPF_MVVM.Services
{
    internal class DataService
    {
        private const string dataSourceURL =
            @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";


        private static async Task<Stream> GetDataStream()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(dataSourceURL, HttpCompletionOption.ResponseHeadersRead);
            return await response.Content.ReadAsStreamAsync();
        }

        private static async Task<IEnumerable<string>> GetDataLines()
        {
            using var data_stream = await GetDataStream();
            using var data_reader = new StreamReader(data_stream);

            var lines = new List<string>();
            while (!data_reader.EndOfStream)
            {
                var line = await data_reader.ReadLineAsync();
                if (!string.IsNullOrWhiteSpace(line))
                 lines.Add(line.Replace("Korea,", "Korea-"));
            }
            return lines;
        }

        private static async Task<DateTime[]> GetDates()
        {
            var lines = await GetDataLines();
              return lines.First()
                .Split(',')
                .Skip(4)
                .Select(s => DateTime.Parse(s, CultureInfo.InvariantCulture))
                .ToArray();


        } 

        private static async Task<IEnumerable<(string Province, string Country, (double Lat, double Lon) Place, int[] Counts)>> GetCountryData()
        {

            var lines = (await GetDataLines())
                .Skip(1)
                .Select(line => line.Split(','));
            return lines.Select(row =>
            {
                var province = row[0].Trim();
                var country_name = row[1].Trim(' ', '"');
                var latitude = double.Parse(row[2]);
                var longitude = double.Parse(row[3]);
                var counts = row.Skip(5)
                    .Select(int.Parse).ToArray();
                return (province, country_name, (latitude, longitude), counts);
            });
        }

        public async Task<IEnumerable<CountryInfo>> GetData()
        {
            var dates = await GetDates();
            var data = (await GetCountryData()).GroupBy(d => d.Country);
            var countries = new List<CountryInfo>();

            foreach (var country_info in data)
            {
             
                var country = new CountryInfo()
                {
                    Name = country_info.Key,
                    ProvinceCounts = country_info.Select(c=>new PlaceInfo()
                    {
                        Name = c.Province,
                        Location = new Point(c.Place.Lat, c.Place.Lon),
                        Counts = dates.Zip(c.Counts, (date, count) => new ConfirmedCount{Date = date, Count = count})
                    })
                };
                countries.Add(country);
            }
            return countries;
        }
    }
}
