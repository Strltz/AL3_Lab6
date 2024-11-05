using Lab6;
using Newtonsoft.Json;
using System.Net;

// https://api.openweathermap.org/data/2.5/weather?lat=70&lon=73&appid={your api key}

List<Weather> weatherList = new List<Weather>();
string api_key = "y0ur_4p1_k3y";

Random rnd = new Random();
Console.WriteLine("start");

for (int i = 0; i < 50; ++i)
{
    double latitude = rnd.Next(-90, 90) + rnd.NextDouble();
    double longitude = rnd.Next(-180, 180) + rnd.NextDouble();
    string url = 
        $"https://api.openweathermap.org/data/2.5/weather?lat=" +
        $"{latitude}&lon={longitude}&appid={api_key}";

    {
        WebClient client = new WebClient();
        string json_file = client.DownloadString(url);
        dynamic data = JsonConvert.DeserializeObject(json_file);
        Weather new_weather = new Weather((string)data.sys.country,
            (string)data.name, Math.Round((double)data.main.temp - 273.16, 1),
            (string)data.weather[0].description);

        if (string.IsNullOrEmpty(new_weather.Country) ||
            string.IsNullOrEmpty(new_weather.Name))
        {
            --i;
            continue;
        }
        weatherList.Add(new_weather);
    }
    Console.WriteLine($"{i * 2}%");
}
Console.WriteLine("100%");

foreach (Weather i in weatherList)
{
    Console.WriteLine($"Country: {i.Country}; Name: {i.Name}; Temp: {i.Temp}; " +
        $"Description: {i.Description};");
}

Console.WriteLine();
Console.WriteLine("---------------------------------------------------------");

var sorted_weather = weatherList.OrderBy(n => n.Temp);
int len_w = sorted_weather.Count();

/*foreach (Weather i in sorted_weather)
{
    Console.WriteLine($"Country: {i.Country}; Name: {i.Name}; Temp: {i.Temp}; " +
        $"Description: {i.Description};");
}*/

// number 1
Console.WriteLine("Country with minimal temperature");
Console.WriteLine($" {sorted_weather.First<Weather>().Country};" +
    /*$"Name: {sorted_weather.First<Weather>().Name};*/ $"Temp:" +
    $"{sorted_weather.First<Weather>().Temp}");
Console.WriteLine();
Console.WriteLine("Country with maximal temperature");
Console.WriteLine($" {sorted_weather.Last<Weather>().Country}; " +
    /*$"Name: {sorted_weather.Last<Weather>().Name};*/ $"Temp:" +
    $"{sorted_weather.Last<Weather>().Temp}");
Console.WriteLine("---------------------------------------------------------");

// number 2
Console.WriteLine($"Average temperature: " +
    $"{Math.Round(sorted_weather.Average<Weather>(n => n.Temp), 1)}");
Console.WriteLine("---------------------------------------------------------");

// number 3
{
    List<string> countries = new List<string>();
    foreach (Weather i in sorted_weather)
    {
        countries.Add(i.Country);
    }
    var countries_counter = countries.Distinct<string>();
    Console.WriteLine($"Number of countries: {countries_counter.Count()}");
}
Console.WriteLine("---------------------------------------------------------");

// number 4
{
    var description_rain = sorted_weather.SkipWhile<Weather>(n =>
        n.Description != "rain");
    var description_clear_sky = sorted_weather.SkipWhile<Weather>(n =>
        n.Description != "clear sky");
    var description_few_clouds = sorted_weather.SkipWhile<Weather>(n =>
        n.Description != "few clouds");

    if (description_clear_sky.Count() > 0)
    {
        Console.WriteLine($"In {description_clear_sky.First().Name}, " +
            $"{description_clear_sky.First().Country} is clear sky");
    }
    else { Console.WriteLine("Clear sky: -"); }

    if (description_rain.Count() > 0)
    {
        Console.WriteLine($"In {description_rain.First().Name}, " +
        $"{description_rain.First().Country} is rain");
    }
    else { Console.WriteLine("Rain: -"); }

    if (description_few_clouds.Count() > 0)
    {
        Console.WriteLine($"In {description_few_clouds.First().Name}, " +
        $"{description_few_clouds.First().Country} is few clouds");
    }
    else { Console.WriteLine("Few clouds: -"); }
}
