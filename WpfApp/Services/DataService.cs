using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Windows;
using WpfApp.Models;

namespace WpfApp.Services
{
    internal class DataService
    {
        const string _DataSourceAddress = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";

        private static async Task<Stream> GetDataStream()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(_DataSourceAddress, HttpCompletionOption.ResponseHeadersRead);
            return await response.Content.ReadAsStreamAsync();
        }

        private static IEnumerable<string> GetDataLines()
        {
            using var dataStream = GetDataStream().Result;
            using var dataReader = new StreamReader(dataStream);

            while (!dataReader.EndOfStream)
            {
                var line = dataReader.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) continue;
                line = line.Replace("Bonaire,", "Bonaire -");
                line = line.Replace("Korea,", "Korea -");
                yield return line;
            }
        }

        private static DateTime[] GetDates() => GetDataLines()
            .First()
            .Split(',')
            .Skip(4)
            .Select(s => DateTime.Parse(s, CultureInfo.InvariantCulture))
            .ToArray();

        private static IEnumerable<(string Province, string Country, (double Lan, double Lon) Place, int[] InfectedCount)> GetCountriesData()
        {
            var lines = GetDataLines()
                .Skip(1)
                .Select(line => line.Split(","));

            foreach (var line in lines)
            {
                var province = line[0].Trim();
                var country = line[1].Trim(' ', '"');
                var latitude = double.Parse(line[2]);
                var longitude = double.Parse(line[3]);
                var infectedCount = line.Skip(4).Select(int.Parse).ToArray();

                yield return (province, country, (latitude, longitude), infectedCount);
            }
        }

        public IEnumerable<CountryInfo> GetData()
        {
            var dates = GetDates();

            var data = GetCountriesData().GroupBy(d => d.Country);

            foreach (var country_info in data)
            {
                var country = new CountryInfo
                {
                    Name = country_info.Key,
                    ProvinceCounts = country_info.Select(c => new PlaceInfo
                    {
                        Name = c.Province,
                        Location = new Point(c.Place.Lan, c.Place.Lon),
                        InfectedCounts = dates.Zip(c.InfectedCount, (date, count) =>
                                         new InfectedCount { Date = date, Count = count })
                    })
                };
                yield return country;              
            }
        }
    }
}
