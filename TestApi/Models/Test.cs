using System;
using System.Text.Json.Serialization;

namespace DynamoDbTestApi.Models;

public class Test
{
    public Test()
    {
        Id = Guid.NewGuid();
    }

    [JsonPropertyName("pk")]
    public string Pk => Id.ToString();

    [JsonPropertyName("sk")]
    public string Sk => Id.ToString();

    public Guid Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public DateTime? UpdateAt { get; set; }
}
