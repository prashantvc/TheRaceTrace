using System.Text.Json.Serialization;

public partial class ConstructorTable
{
    [JsonPropertyName("Constructors")]
    public Constructor[] Constructors { get; set; }
}

public partial class Constructor
{
    [JsonPropertyName("constructorId")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("nationality")]
    public string Nationality { get; set; }

    public override string ToString()
    {
        return $"ID: {Id}, Name: {Name}";
    }
}