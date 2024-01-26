using System.Text.Json.Serialization;

public partial class ConstructorTable
{
    [JsonPropertyName("Constructors")]
    public Constructor[] Constructors { get; set; }
}

public partial class Constructor
{
    [JsonPropertyName("constructorId")]
    public string ConstructorId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("nationality")]
    public string Nationality { get; set; }
}