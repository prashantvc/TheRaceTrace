using System.Text.Json.Serialization;

public partial class DriverTable
{
    [JsonPropertyName("season")]
    [JsonConverter(typeof(ParseStringConverter))]
    public int Season { get; set; }

    [JsonPropertyName("round")]
    [JsonConverter(typeof(ParseStringConverter))]
    public int Round { get; set; }

    [JsonPropertyName("Drivers")]
    public Driver[] Drivers { get; set; }
}

public partial class Driver
{
    [JsonPropertyName("driverId")]
    public string DriverId { get; set; }

    [JsonPropertyName("permanentNumber")]
    [JsonConverter(typeof(ParseStringConverter))]
    public int PermanentNumber { get; set; }

    [JsonPropertyName("code")]
    public string Code { get; set; }

    [JsonPropertyName("url")]
    public Uri Url { get; set; }

    [JsonPropertyName("givenName")]
    public string GivenName { get; set; }

    [JsonPropertyName("familyName")]
    public string FamilyName { get; set; }

    [JsonPropertyName("dateOfBirth")]
    public DateTimeOffset DateOfBirth { get; set; }

    [JsonPropertyName("nationality")]
    public string Nationality { get; set; }
}