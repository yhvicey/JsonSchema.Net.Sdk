using System.Collections.Generic;
using JsonSchema.Net.Sdk.Generators;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace JsonSchema.Net.Sdk.Tasks
{
    public class GenerateFile : Task
    {
        public ITaskItem[] JsonSchemaFiles { get; set; }

        public override bool Execute()
        {
            // Setup Logger
            Utils.Logger.Init(Log);

            foreach (var taskItem in JsonSchemaFiles)
            {
                var jsonSchemaItem = new JsonSchemaItem(taskItem);
                foreach (var generator in Generators)
                {
                    // Generate file
                    var outputFilePath = jsonSchemaItem.GetOutputFilePath(generator.TargetLanguage);
                    Utils.Logger.LogMessage($"Generating {jsonSchemaItem.SchemaFile} to {outputFilePath}.");
                    if (!generator.Generate(jsonSchemaItem))
                    {
                        Utils.Logger.LogError($"Failed to generate {jsonSchemaItem.SchemaFile} to {outputFilePath}.");
                        return false;
                    }
                }
            }
            return true;
        }

        private readonly IList<GeneratorBase> Generators = new List<GeneratorBase>
        {
            new CSGenerator(),
            new TSGenerator(),
        };
    }
}