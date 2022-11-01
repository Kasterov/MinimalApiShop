﻿using System.Text.Json.Serialization;

namespace MinimalApiShop.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Category
{
    Home,
    Work,
    Garden,
    Car,
    Sport
}
