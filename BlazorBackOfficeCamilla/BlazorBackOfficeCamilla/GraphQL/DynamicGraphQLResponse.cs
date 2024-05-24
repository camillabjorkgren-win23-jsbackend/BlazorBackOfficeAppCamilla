using System.Text.Json;
using System.Text.Json.Serialization;

namespace BlazorBackOfficeCamilla.GraphQL;

public class DynamicGraphQLResponse
{
    [JsonPropertyName("data")]
    public JsonElement Data { get; set; }
}
