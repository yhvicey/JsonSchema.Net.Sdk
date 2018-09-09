using System;
using Microsoft.Build.Framework;
using NJsonSchema;
using NJsonSchema.CodeGeneration.CSharp;

namespace JsonSchema.Net.Sdk
{
    public class GenerateCSFile : GenerateFile<CSharpGenerator, CSharpGeneratorSettings>
    {
        protected override string GeneratedFileExtension => ".cs";

        protected override CSharpGenerator GetGenerator(string schemaFile)
        {
            var schema = JsonSchema4.FromFileAsync(schemaFile).Result;
            var settings = new CSharpGeneratorSettings
            {
                Namespace = Namespace
            };
            return new CSharpGenerator(schema, settings);
        }

        protected override bool ShouldGenerate(ITaskItem taskItem)
        {
            var generateCSFileValue = taskItem.GetMetadata("GenerateCSFile");
            return !generateCSFileValue.Equals("false", StringComparison.OrdinalIgnoreCase);
        }
    }
}