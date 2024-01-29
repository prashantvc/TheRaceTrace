using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

public partial class RaceData
{
    [JsonPropertyName("MRData")]
    public MrData MrData { get; set; }
}

public partial class MrData
{
    [JsonPropertyName("url")]
    public Uri Url { get; set; }

    [JsonPropertyName("total")]
    [JsonConverter(typeof(ParseStringConverter))]
    public int TotalLaps { get; set; }

    [JsonPropertyName("RaceTable")]
    public RaceTable RaceTable { get; set; }
    
    [JsonPropertyName("DriverTable")]
    public DriverTable DriverTable { get; set; }
    
    [JsonPropertyName("ConstructorTable")]
    public ConstructorTable ConstructorTable { get; set; }
}

public partial class RaceTable
{
    [JsonPropertyName("season")]
    [JsonConverter(typeof(ParseStringConverter))]
    public int Season { get; set; }

    [JsonPropertyName("round")]
    [JsonConverter(typeof(ParseStringConverter))]
    public int Round { get; set; }

    [JsonPropertyName("Races")]
    public Race[] Races { get; set; }
}

public partial class Race
{
    [JsonPropertyName("season")]
    public string Season { get; set; }

    [JsonPropertyName("round")]
    [JsonConverter(typeof(ParseStringConverter))]
    public int Round { get; set; }

    [JsonPropertyName("url")]
    public Uri Url { get; set; }

    [JsonPropertyName("raceName")]
    public string RaceName { get; set; }

    [JsonPropertyName("Circuit")]
    public Circuit Circuit { get; set; }

    [JsonPropertyName("date")]
    public string Date { get; set; }

    [JsonPropertyName("time")]
    public string Time { get; set; }

    [JsonPropertyName("Laps")]
    public Lap[] Laps { get; set; }

    override public string ToString()
    {
        return $"{Round} - {RaceName}";
    }
}

public partial class Circuit
{
    [JsonPropertyName("circuitId")]
    public string CircuitId { get; set; }

    [JsonPropertyName("url")]
    public Uri Url { get; set; }

    [JsonPropertyName("circuitName")]
    public string CircuitName { get; set; }

    [JsonPropertyName("Location")]
    public Location Location { get; set; }
}

public partial class Location
{
    [JsonPropertyName("lat")]
    public string Latitude { get; set; }

    [JsonPropertyName("long")]
    public string Longitude { get; set; }

    [JsonPropertyName("locality")]
    public string Locality { get; set; }

    [JsonPropertyName("country")]
    public string Country { get; set; }
}

public partial class Lap
{
    [JsonPropertyName("number")]
    [JsonConverter(typeof(ParseStringConverter))]
    public int Number { get; set; }

    [JsonPropertyName("Timings")]
    public Timing[] Timings { get; set; }
}

public partial class Timing
{
    [JsonPropertyName("driverId")]
    public string DriverId { get; set; }

    [JsonPropertyName("position")]
    [JsonConverter(typeof(ParseStringConverter))]
    public int Position { get; set; }

    [JsonPropertyName("time")]
    [JsonConverter(typeof(ParseTimeConverter))]
    public double Time { get; set; }
}

internal class ParseTimeConverter : JsonConverter<double>
{
    public override double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        var result = TimeSpan.TryParseExact(value, @"m\:ss\.fff", CultureInfo.InvariantCulture, 
            out var timeSpan);

        if (result)
            return timeSpan.TotalSeconds;
        
        throw new Exception($"Cannot parse {value} to TimeSpan");
    }

    public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}

internal class ParseStringConverter : JsonConverter<int>
{
    public override bool CanConvert(Type t) => t == typeof(int);

    public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        if (int.TryParse(value, out var l))
        {
            return l;
        }
        throw new Exception("Cannot unmarshal type int");
    }

    public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}