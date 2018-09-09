using System;
using System.IO;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using NJsonSchema.CodeGeneration;

namespace JsonSchema.Net.Sdk
{
    public abstract class GenerateFile<TGenerator, TGeneratorSettings> : Task
        where TGenerator : GeneratorBase
        where TGeneratorSettings : CodeGeneratorSettingsBase
    {
        public bool Flatten { get; set; }

        [Required]
        public ITaskItem[] JsonSchemaFiles { get; set; }

        public string Namespace { get; set; }

        [Required]
        public string To { get; set; }

        public override bool Execute()
        {
            var generateResult = true;
            foreach (var taskItem in JsonSchemaFiles)
            {
                if (!ShouldGenerate(taskItem))
                {
                    continue;
                }

                var flatten = Flatten;
                var flattenValue = taskItem.GetMetadata("Flatten");
                if (bool.TryParse(flattenValue, out var flattenOverride))
                {
                    flatten = flattenOverride;
                }

                // Normalize path
                var schemaFile = Path.GetFullPath(taskItem.ItemSpec);
                var schemaFileNameWithoutExtension = Path.GetFileNameWithoutExtension(taskItem.ItemSpec);
                var schemaFileDirectoryName = Path.GetDirectoryName(taskItem.ItemSpec);

                var toPath = Path.GetFullPath(To);
                var toFile = flatten
                    ? Path.Combine(toPath, $"{schemaFileNameWithoutExtension}{GeneratedFileExtension}")
                    : Path.Combine(toPath, schemaFileDirectoryName, $"{schemaFileNameWithoutExtension}{GeneratedFileExtension}");

                var toDirectory = Path.GetDirectoryName(toFile);
                if (!Directory.Exists(toDirectory))
                {
                    Directory.CreateDirectory(toDirectory);
                }

                // Generate file
                Log.LogMessage($"Generating {schemaFile} to {toPath}.");
                try
                {
                    var generator = GetGenerator(schemaFile);
                    var code = generator.GenerateFile();
                    File.WriteAllText(toFile, code);
                }
                catch (Exception ex)
                {
                    Log.LogErrorFromException(ex, false, true, schemaFile);
                    generateResult = false;
                }
            }
            return generateResult;
        }

        protected abstract string GeneratedFileExtension { get; }

        protected abstract TGenerator GetGenerator(string schemaFile);

        protected abstract bool ShouldGenerate(ITaskItem taskItem);
    }
}