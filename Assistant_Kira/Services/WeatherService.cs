﻿using System.Text.Json;

using Assistant_Kira.Models.OpenWeatherMap;

namespace Assistant_Kira.Services;

internal sealed class WeatherService(ILogger<WeatherService> logger, IConfiguration configuration, IHttpClientFactory httpClientFactory)
{
    public async Task<Weather> GetWeatherAsync(string city = "Astana")
	{
		var response = await httpClientFactory.CreateClient("OpenWeather")
			.GetAsync(new Uri(@$"weather?q={city}&units=metric&lang=ru&appid={configuration["WeatherToken"]}", UriKind.Relative));

		if (!response.IsSuccessStatusCode)
		{
			throw new HttpRequestException();
		}
		try
		{
			var weather = JsonSerializer.Deserialize<Weather>(await response.Content.ReadAsStringAsync());
			return weather;
		}
		catch (ArgumentNullException ex)
		{
            logger.LogError(ex, "При десерелизации произошла ошибка");
			throw;
		}
		catch (NotSupportedException ex)
		{
            logger.LogError(ex, "При десирелизации произошла ошибка");
            throw;
		}
	}
}
