using System.IO;
using JsonSchema.Net.Sdk.Utils;

namespace JsonSchema.Net.Sdk.Generators
{
    public abstract class GeneratorBase
    {
        public abstract TargetLanguage TargetLanguage { get; }

        public bool Generate(JsonSchemaItem item)
        {
            if (!item.MetadataMap.TryGetValue(TargetLanguage, out var metadata))
            {
                Logger.LogError($"Failed to get metadata for target language {TargetLanguage}");
                return false;
            }

            if (!metadata.GenerateFile)
            {
                return true;
            }

            var outputFilePath = item.GetOutputFilePath(TargetLanguage);
            var outputDirectory = Path.GetDirectoryName(outputFilePath);
            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }
            Logger.LogMessage($"Generating {item.SchemaFile} to {outputFilePath}.");
            return Generate(item, metadata);
        }

        protected abstract bool Generate(JsonSchemaItem item, JsonSchemaItemMetadata metadata);
    }
}