using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SteamMicroservice.Model.Games;

namespace SteamMicroservice.Utils
{
    public static class Utils
    {
        public static async IAsyncEnumerable<T> ConvertToAsyncEnumerable<T>(this IEnumerable<T> enumerable)
        {
            foreach (var elemento in enumerable)
            {
                yield return elemento;
                await Task.Yield();
            }
        }
    }

    public class MacRequirementsConverter : JsonConverter<Mac_Requirements>
    {
        public override Mac_Requirements ReadJson(JsonReader reader, Type objectType, Mac_Requirements existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartObject)
            {
                var jObject = JObject.Load(reader);
                return new Mac_Requirements
                {
                    minimum = jObject.GetValue("minimum")?.ToString() ?? string.Empty,
                    recommended = jObject.GetValue("recommended")?.ToString() ?? string.Empty
                };
            }
            else if (reader.TokenType == JsonToken.StartArray)
            {
                // Consume the array token and move to the next token (which should be EndArray)
                reader.Skip();
                return new Mac_Requirements
                {
                    minimum = string.Empty,
                    recommended = string.Empty
                };
            }
            else
            {
                throw new JsonSerializationException($"Unexpected token when deserializing object: {reader.TokenType}. Path '{reader.Path}'.");
            }
        }

        public override void WriteJson(JsonWriter writer, Mac_Requirements value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }


    public class PcRequirementsConverter : JsonConverter<Pc_Requirements>
    {
        public override Pc_Requirements ReadJson(JsonReader reader, Type objectType, Pc_Requirements existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartObject)
            {
                var jObject = JObject.Load(reader);
                return new Pc_Requirements
                {
                    minimum = jObject.GetValue("minimum")?.ToString() ?? string.Empty,
                    recommended = jObject.GetValue("recommended")?.ToString() ?? string.Empty
                };
            }
            else if (reader.TokenType == JsonToken.StartArray)
            {
                // Consume the array token and move to the next token (which should be EndArray)
                reader.Skip();
                return new Pc_Requirements
                {
                    minimum = string.Empty,
                    recommended = string.Empty
                };
            }
            else
            {
                throw new JsonSerializationException($"Unexpected token when deserializing object: {reader.TokenType}. Path '{reader.Path}'.");
            }
        }

        public override void WriteJson(JsonWriter writer, Pc_Requirements value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

    public class LinuxRequirementsConverter : JsonConverter<Linux_Requirements>
    {
        public override Linux_Requirements ReadJson(JsonReader reader, Type objectType, Linux_Requirements existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartObject)
            {
                var jObject = JObject.Load(reader);
                return new Linux_Requirements
                {
                    minimum = jObject.GetValue("minimum")?.ToString() ?? string.Empty,
                    recommended = jObject.GetValue("recommended")?.ToString() ?? string.Empty
                };
            }
            else if (reader.TokenType == JsonToken.StartArray)
            {
                // Consume the array token and move to the next token (which should be EndArray)
                reader.Skip();
                return new Linux_Requirements
                {
                    minimum = string.Empty,
                    recommended = string.Empty
                };
            }
            else
            {
                throw new JsonSerializationException($"Unexpected token when deserializing object: {reader.TokenType}. Path '{reader.Path}'.");
            }
        }

        public override void WriteJson(JsonWriter writer, Linux_Requirements value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
