using System.Text.Json;
using System.Text.Json.Serialization;

namespace Product.Application
{
    public class SerializerOptions
    {
        /// <summary>
        /// Get json serializer options
        /// </summary>
        /// <param name="customConverters">List of custom json converters</param>
        /// <returns></returns>
        public static JsonSerializerOptions DefaultSerializerOptions(params JsonConverter[] customConverters)
        {
            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            // Add json converters which are required across all projects
            var jsonConverters = new List<JsonConverter>
            {
                new JsonStringEnumConverter(),
                //new DateOnlyConverter()
            };

            if (customConverters != null && customConverters.Any())
                jsonConverters.AddRange(customConverters);

            jsonConverters.ForEach(x => option.Converters.Add(x));

            return option;
        }
    }
}