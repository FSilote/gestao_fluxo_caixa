namespace CodeChallenger.Sdk.WebApi.Swagger.Filters
{
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;

    public class SwaggerIgnorePropertyFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema?.Properties == null || context == null || context.Type == null)
                return;

            var type = context.Type;

            var excludedProperties = type.GetProperties()
                                         .Where(t => t.CustomAttributes.Any(a =>
                                            a.AttributeType == typeof(System.Text.Json.Serialization.JsonIgnoreAttribute)
                                            || a.AttributeType == typeof(Newtonsoft.Json.JsonIgnoreAttribute)))
                                         .ToList();

            foreach (var excludedProperty in excludedProperties)
            {
                if (schema.Properties.ContainsKey(this.ToCamelCase(excludedProperty.Name)))
                {
                    schema.Properties.Remove(this.ToCamelCase(excludedProperty.Name));
                }
            }
        }

        private string ToCamelCase(string text)
        {
            return $"{text[0].ToString().ToLower()}{text.Remove(0, 1)}";
        }
    }
}
