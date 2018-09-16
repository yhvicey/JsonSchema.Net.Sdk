using Microsoft.Build.Framework;

namespace JsonSchema.Net.Sdk
{
    public class JsonSchemaItemMetadata
    {
        public string FileOutputPath { get; set; }
        public bool Flatten { get; set; }
        public bool GenerateFile { get; set; }
        public string Namespace { get; set; }
        public TargetLanguage TargetLanguage { get; }

        public JsonSchemaItemMetadata(ITaskItem taskItem, TargetLanguage targetLanguage)
        {
            TargetLanguage = targetLanguage;
            FileOutputPath = GetMetadata(taskItem, TargetLanguage, nameof(FileOutputPath));
            Flatten = GetMetadataAsBoolean(taskItem, TargetLanguage, nameof(Flatten));
            GenerateFile = GetMetadataAsBoolean(taskItem, TargetLanguage, nameof(GenerateFile));
            Namespace = GetMetadata(taskItem, TargetLanguage, nameof(Namespace));
        }

        private static string GetMetadata(ITaskItem taskItem, TargetLanguage targetLanguage, string metadataName)
        {
            // Get metadata
            var metadata = taskItem?.GetMetadata($"{metadataName}");
            // Get prefixed metadata
            var prefixedMetadata = taskItem?.GetMetadata($"{targetLanguage}{metadataName}");
            return string.IsNullOrWhiteSpace(prefixedMetadata)
                ? metadata
                : prefixedMetadata;
        }

        private static bool GetMetadataAsBoolean(ITaskItem taskItem, TargetLanguage prefix, string metadataName)
            => bool.TryParse(GetMetadata(taskItem, prefix, metadataName), out var value)
            ? value
            : default(bool);
    }
}