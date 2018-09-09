using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JsonSchema.Net.Sdk.UnitTests
{
    [TestClass]
    public class FileExistanceTest
    {
        [TestMethod]
        public void ValidateCSFileExists()
        {
            foreach (var fileName in csFileNames)
            {
                var filePath = Path.GetFullPath(Path.Combine(SampleProjectGenerateRoot, "cs", "Schema", $"{fileName}.cs"));
                Assert.IsTrue(File.Exists(filePath), $"{filePath} doesn't exist.");
            }
            foreach (var fileName in csFlattenedFileNames)
            {
                var filePath = Path.GetFullPath(Path.Combine(SampleProjectGenerateRoot, "cs", $"{fileName}.cs"));
                Assert.IsTrue(File.Exists(filePath), $"{filePath} doesn't exist.");
            }
        }

        [TestMethod]
        public void ValidateTSFileExists()
        {
            foreach (var fileName in tsFileNames)
            {
                var filePath = Path.GetFullPath(Path.Combine(SampleProjectGenerateRoot, "ts", "Schema", $"{fileName}.ts"));
                Assert.IsTrue(File.Exists(filePath), $"{filePath} doesn't exist.");
            }
            foreach (var fileName in tsFlattenedFileNames)
            {
                var filePath = Path.GetFullPath(Path.Combine(SampleProjectGenerateRoot, "ts", $"{fileName}.ts"));
                Assert.IsTrue(File.Exists(filePath), $"{filePath} doesn't exist.");
            }
        }

        private static readonly string[] csFileNames = new[]
        {
            "User",
        };

        private static readonly string[] csFlattenedFileNames = new[]
        {
            "Entity",
        };

        private static readonly string SampleProjectGenerateRoot = Path.Combine(
            Directory.GetCurrentDirectory(), // netcorex.x
            "..", // $(Configuration)
            "..", // bin
            "..", // $(ProjectRoot)
            "..",
            "SampleProject",
            "generate");

        private static readonly string[] tsFileNames = new[]
                {
            "Message",
        };

        private static readonly string[] tsFlattenedFileNames = new[]
        {
            "Entity",
        };
    }
}