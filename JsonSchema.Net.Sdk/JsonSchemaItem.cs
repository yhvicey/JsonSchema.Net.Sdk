using System;
using System.Collections.Generic;
using System.IO;
using JsonSchema.Net.Sdk.Utils;
using Microsoft.Build.Framework;

namespace JsonSchema.Net.Sdk
{
    public class JsonSchemaItem
    {
        public IDictionary<TargetLanguage, JsonSchemaItemMetadata> MetadataMap { get; } = new Dictionary<TargetLanguage, JsonSchemaItemMetadata>();
        public string SchemaFile { get; }

        public JsonSchemaItem(ITaskItem taskItem)
        {
            SchemaFile = Path.GetFullPath(taskItem.ItemSpec);
            foreach (TargetLanguage targetLanguage in Enum.GetValues(typeof(TargetLanguage)))
            {
                MetadataMap[targetLanguage] = new JsonSchemaItemMetadata(taskItem, targetLanguage);
            }
        }

        public string GetOutputFilePath(TargetLanguage targetLanguage)
        {
            if (!MetadataMap.TryGetValue(targetLanguage, out var metadata))
            {
                Logger.LogError($"Failed to get metadata for target language {targetLanguage}.");
                return null;
            }

            var currentFolder = Directory.GetCurrentDirectory();

            var schemaFileDirectoryName = Path.GetDirectoryName(SchemaFile);

            var outputPath = Path.GetFullPath(metadata.FileOutputPath);
            var outputFileName = $"{Path.GetFileNameWithoutExtension(SchemaFile)}.{targetLanguage.ToString().ToLower()}";
            if (File.Exists(outputPath))
            {
                Logger.LogError($"{nameof(JsonSchemaItemMetadata.FileOutputPath)} must be a folder.");
                return null;
            }

            var isUnderProjectFolder = schemaFileDirectoryName.IndexOf(currentFolder) == 0;
            if (!isUnderProjectFolder && metadata.Flatten)
            {
                Logger.LogWarning("Schema file is not under project path, flatten will be skipped.");
            }

            var shouldAppendRelativePath = isUnderProjectFolder && !metadata.Flatten;

            return shouldAppendRelativePath
                ? Path.Combine(outputPath, schemaFileDirectoryName.Remove(0, currentFolder.Length + 1), outputFileName)
                : Path.Combine(outputPath, outputFileName);
        }
    }
}