using System.Text.Json;
using System.Text.Json.Serialization;

namespace PlanetaryExplorationLogs.API.Converters
{
  public class StringJsonConverter : JsonConverter<string?>
  {
    public override string? Read(
      ref Utf8JsonReader reader,
      Type typeToConvert,
      JsonSerializerOptions options
    )
    {
      var value = reader.GetString();

      if (value != null)
      {
        value = value.Trim();

        if (value.Length == 0)
        {
          value = null;
        }
      }

      return value;
    }

    public override void Write(
      Utf8JsonWriter writer,
      string? value,
      JsonSerializerOptions options
    ) =>
        writer.WriteStringValue(value);
  }
}
