using System.Globalization;

internal class Program
{
    const string data_url = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";

    private static async Task<Stream> GetDataStream()
    {
        var client = new HttpClient();
        var response = await client.GetAsync(data_url, HttpCompletionOption.ResponseHeadersRead);
        return await response.Content.ReadAsStreamAsync();
    }

    private static IEnumerable<string> GetDataLines()
    {
        using var dataStream = GetDataStream().Result;
        using var dataReader = new StreamReader(dataStream);

        while(!dataReader.EndOfStream)
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

    private static IEnumerable<(string Country, string Province, int[] InfectedCount)> GetData()
    {
        var lines = GetDataLines()
            .Skip(1)
            .Select(line => line.Split(","));

        foreach (var line in lines)
        {
            var province = line[0].Trim();
            var country = line[1].Trim(' ', '"');
            var infectedCount = line.Skip(4).Select(int.Parse).ToArray();

            yield return (country, province, infectedCount);
        }
    }

    private static void Main(string[] args)
    {
        //WebClient client = new WebClient(); 

        //var client = new HttpClient();

        //var response = client.GetAsync(data_url).Result;
        //var csv_str = response.Content.ReadAsStringAsync().Result;

        //foreach (var line in GetDataLines())
        //{
        //    Console.WriteLine(line);
        //}

        //var dates = GetDates();
        //Console.WriteLine(string.Join("\r\n", dates));

        var russia_data = GetData()
            .First(v => v.Country.Equals("Russia", StringComparison.OrdinalIgnoreCase));

        Console.WriteLine(string.Join("\r\n", GetDates().Zip(russia_data.InfectedCount, (date, count) => $"{date} : {count}")));


    }
}