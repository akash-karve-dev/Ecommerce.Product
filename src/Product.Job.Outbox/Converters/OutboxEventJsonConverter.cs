using Product.Job.Outbox.Models.Incoming;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Product.Job.Outbox.Converters
{
    internal sealed class OutboxEventJsonConverter : JsonConverter<BaseOutboxEvent>
    {
        public override BaseOutboxEvent Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var jd = JsonDocument.ParseValue(ref reader);

            if (!jd.RootElement.TryGetProperty("discriminator", out JsonElement val))
            {
                throw new Exception("Discriminator property not found");
            }

            var type = val.GetString();

            switch (type)
            {
                case "OutboxEntity":
                    return jd.Deserialize<OutboxEntityEvent>(options);

                default:
                    return null;
            }
        }

        public override void Write(Utf8JsonWriter writer, BaseOutboxEvent value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}