using System.Text.Json;
using System.Text.Json.Serialization;
using System.Globalization;

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
    
    [JsonPropertyName("Drivers")]
    public DriverTable DriverTable { get; set; }
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
    public string Round { get; set; }

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
    public string Time { get; set; }
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
        JsonSerializer.Serialize(writer, value.ToString(), options);
    }
}

public class DateOnlyConverter(string? serializationFormat) : JsonConverter<DateOnly>
{
    private readonly string _serializationFormat = serializationFormat ?? "yyyy-MM-dd";
    public DateOnlyConverter() : this(null) { }

    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return DateOnly.Parse(value!);
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.ToString(_serializationFormat));
}

public class TimeOnlyConverter(string? serializationFormat) : JsonConverter<TimeOnly>
{
    private readonly string _serializationFormat = serializationFormat ?? "HH:mm:ss.fff";

    public TimeOnlyConverter() : this(null) { }

    public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return TimeOnly.Parse(value!);
    }

    public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.ToString(_serializationFormat));
}

internal class IsoDateTimeOffsetConverter : JsonConverter<DateTimeOffset>
{
    public override bool CanConvert(Type t) => t == typeof(DateTimeOffset);

    private const string DefaultDateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK";

    private DateTimeStyles _dateTimeStyles = DateTimeStyles.RoundtripKind;
    private string? _dateTimeFormat;
    private CultureInfo? _culture;

    public DateTimeStyles DateTimeStyles
    {
        get => _dateTimeStyles;
        set => _dateTimeStyles = value;
    }

    public string? DateTimeFormat
    {
        get => _dateTimeFormat ?? string.Empty;
        set => _dateTimeFormat = (string.IsNullOrEmpty(value)) ? null : value;
    }

    public CultureInfo Culture
    {
        get => _culture ?? CultureInfo.CurrentCulture;
        set => _culture = value;
    }

    public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
    {
        string text;


        if ((_dateTimeStyles & DateTimeStyles.AdjustToUniversal) == DateTimeStyles.AdjustToUniversal
            || (_dateTimeStyles & DateTimeStyles.AssumeUniversal) == DateTimeStyles.AssumeUniversal)
        {
            value = value.ToUniversalTime();
        }

        text = value.ToString(_dateTimeFormat ?? DefaultDateTimeFormat, Culture);

        writer.WriteStringValue(text);
    }

    public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? dateText = reader.GetString();

        if (string.IsNullOrEmpty(dateText) == false)
        {
            if (!string.IsNullOrEmpty(_dateTimeFormat))
            {
                return DateTimeOffset.ParseExact(dateText, _dateTimeFormat, Culture, _dateTimeStyles);
            }
            else
            {
                return DateTimeOffset.Parse(dateText, Culture, _dateTimeStyles);
            }
        }
        else
        {
            return default;
        }
    }


    public static readonly IsoDateTimeOffsetConverter Singleton = new();
}
