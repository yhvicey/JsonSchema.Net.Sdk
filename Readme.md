# JsonSchema.Net.Sdk

Sdk for creating json schema project.

## Build

Master|PR
-|-
[![Build status](https://vicey.visualstudio.com/GithubProjectsCICD/_apis/build/status/JsonSchema.Net.Sdk%20-%20master)](https://vicey.visualstudio.com/GithubProjectsCICD/_build/latest?definitionId=6)|[![Build status](https://vicey.visualstudio.com/GithubProjectsCICD/_apis/build/status/JsonSchema.Net.Sdk%20-%20PR)](https://vicey.visualstudio.com/GithubProjectsCICD/_build/latest?definitionId=7)

## Sample

```xml
<Project Sdk="JsonSchema.Net.Sdk/0.2.3">
    <PropertyGroup>
        <Flatten>False</Flatten>
        <!-- CS Generation properties -->
        <CSGenerateFile>True</CSGenerateFile>
        <CSFileOutputPath>generate\cs\</CSFileOutputPath>
        <CSNamespace>SampleProject.CS</CSNamespace>
        <!-- TS Generation properties -->
        <TSGenerateFile>True</TSGenerateFile>
        <TSFileOutputPath>generate\ts\</TSFileOutputPath>
        <TSNamespace>SampleProject.TS</TSNamespace>
    </PropertyGroup>

    <ItemGroup>
        <JsonSchema Include="Schema\Entity.json">
            <Flatten>True</Flatten>
        </JsonSchema>
        <JsonSchema Include="Schema\Message.json">
            <CSGenerateFile>False</CSGenerateFile>
        </JsonSchema>
        <JsonSchema Include="Schema\User.json">
            <TSGenerateFile>False</TSGenerateFile>
        </JsonSchema>
    </ItemGroup>
</Project>
```
