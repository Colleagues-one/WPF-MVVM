using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
            var response = await client.GetAsync(dataSourceURL, 
                HttpCompletionOption.ResponseHeadersRead);
            return await response.Content.ReadAsStreamAsync();
        }

        private static IEnumerable<string> GetDataLines()
        {
            using var data_stream = (SynchronizationContext.Current is null ? GetDataStream() : Task.Run(GetDataStream)).Result;
            using var data_reader = new StreamReader(data_stream);
            while (!data_reader.EndOfStream)
            {
                var line = data_reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) continue;
                yield return line
                    .Replace("Korea,", "Korea-")
                    .Replace("Bonaire,", "Bonaire -")
                    .Replace("Helena,", "Helena -");
            }
        }

        private static DateTime[] GetDates() => GetDataLines()
            .First()
            .Split(',')
            .Skip(4)
            .Select(s => DateTime.Parse(s, CultureInfo.InvariantCulture))
            .ToArray();

        private static IEnumerable<(string Province, string Country, (double Lat, double Lon) Place, int[] Counts)>
            GetCountryData()
        {

            var lines = GetDataLines()
                .Skip(1)
                .Select(line => line.Split(','));
            foreach (var row in lines)
            {
                var province = row[0].Trim();
                var country_name = row[1].Trim(' ', '"');
                var latitude = (double.TryParse(row[2], CultureInfo.InvariantCulture, out double number))?number:default;
                var longitude = (double.TryParse(row[3], CultureInfo.InvariantCulture, out double number2))?number2: default;
                var counts = row.Skip(4)
                    .Select(int.Parse)
                    .ToArray();
                yield return (province, country_name, (latitude, longitude), counts);
            }
        }

        public IEnumerable<CountryInfo> GetData()
        {
            var dates = GetDates();
            var data = GetCountryData().GroupBy(d => d.Country);

            foreach (var country_info in data)
            {

                var country = new CountryInfo()
                {
                    Name = country_info.Key,
                    ProvinceCounts = country_info.Select(c => new PlaceInfo
                    {
                        Name = c.Province,
                        Location = new Point(c.Place.Lat, c.Place.Lon),
                        Counts = dates.Zip(c.Counts, (date, count) => new ConfirmedCount { Date = date, Count = count })
                    })
                };
                yield return country;
            }
        }
    }
}