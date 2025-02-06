using Newtonsoft.Json;
using System.Globalization;

namespace OnlineCasino.Service;

public class DecimalJsonConverter : JsonConverter<decimal>
{
	public override void WriteJson(JsonWriter writer, decimal value, JsonSerializer serializer)
	{
		writer.WriteRawValue(value.ToString("0.##", CultureInfo.InvariantCulture));
	}

	public override decimal ReadJson(JsonReader reader, Type objectType, decimal existingValue, bool hasExistingValue, JsonSerializer serializer)
	{
		return Convert.ToDecimal(reader.Value, CultureInfo.InvariantCulture);
	}
}
