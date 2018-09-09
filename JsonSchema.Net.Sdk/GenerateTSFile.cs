using System;
using Microsoft.Build.Framework;
using NJsonSchema;
using NJsonSchema.CodeGeneration.TypeScript;

namespace JsonSchema.Net.Sdk
{
    public class GenerateTSFile : GenerateFile<TypeScriptGenerator, TypeScriptGeneratorSettings>
    {
        protected override string GeneratedFileExtension => ".ts";

        protected override TypeScriptGenerator GetGenerator(string schemaFile)
        {
            var schema = JsonSchema4.FromFileAsync(schemaFile).Result;
            var settings = new TypeScriptGeneratorSettings
            {
                Namespace = Namespace
            };
            return new TypeScriptGenerator(schema, settings);
        }

        protected override bool ShouldGenerate(ITaskItem taskItem)
        {
            var generateTSFileValue = taskItem.GetMetadata("GenerateTSFile");
            return !generateTSFileValue.Equals("false", StringComparison.OrdinalIgnoreCase);
        }
    }
}