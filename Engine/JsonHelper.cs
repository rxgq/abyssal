using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace engine
{
    public static class SerializationHelper
    {
        public static void SaveAsJson(string filePath)
        {
            string skytaieFolderPath = Path.Combine(filePath, "skytaie");

            if (!Directory.Exists(skytaieFolderPath))
                Directory.CreateDirectory(skytaieFolderPath);

            string shapesFilePath = Path.Combine(skytaieFolderPath, "shapes.json");
            string spritesFilePath = Path.Combine(skytaieFolderPath, "sprites.json");

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new CustomContractResolver()
            };

            string shapesJson = JsonConvert.SerializeObject(Engine.Shapes, settings);
            File.WriteAllText(shapesFilePath, shapesJson);

            string spritesJson = JsonConvert.SerializeObject(Engine.Sprites, settings);
            File.WriteAllText(spritesFilePath, spritesJson);
        }
    }

    public class CustomContractResolver : DefaultContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> properties = base.CreateProperties(type, memberSerialization);

            if (type.BaseType != null)
            {
                PropertyInfo[] baseProperties = type.BaseType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo property in baseProperties)
                {
                    if (properties.All(p => p.PropertyName != property.Name))
                    {
                        JsonProperty jsonProperty = CreateProperty(property, memberSerialization);
                        properties.Add(jsonProperty);
                    }
                }
            }

            return properties;
        }
    }
}
