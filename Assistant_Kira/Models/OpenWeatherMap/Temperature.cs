﻿using System.Text.Json.Serialization;

namespace Assistant_Kira.Models.OpenWeatherMap;

internal struct Temperature
{
	[JsonPropertyName("temp")]
	public float Value { get; init; }

	[JsonPropertyName("feels_like")]
	public float FeelsLikeValue { get; init; }

    [JsonConstructor]
    public Temperature(float value, float feelsLikeValue)
    {
        Value = value;
        FeelsLikeValue = feelsLikeValue;
    }
}
