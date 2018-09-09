# JsonSchema.Net.Sdk

Sdk for creating json schema project.

## Sample

```xml
<Project Sdk="JsonSchema.Net.Sdk/0.1.1">
    <PropertyGroup>
        <Flatten>False</Flatten>
        <!-- CS Generation properties -->
        <GenerateCSFile>True</GenerateCSFile>
        <CSFileOutputPath>generate\cs\</CSFileOutputPath>
        <CSNamespace>SampleProject.CS</CSNamespace>
        <!-- TS Generation properties -->
        <GenerateTSFile>True</GenerateTSFile>
        <TSFileOutputPath>generate\ts\</TSFileOutputPath>
        <TSNamespace>SampleProject.TS</TSNamespace>
    </PropertyGroup>

    <ItemGroup>
        <JsonSchema Include="Schema\Entity.json">
            <Flatten>True</Flatten>
        </JsonSchema>
        <JsonSchema Include="Schema\Message.json">
            <GenerateCSFile>False</GenerateCSFile>
        </JsonSchema>
        <JsonSchema Include="Schema\User.json">
            <GenerateTSFile>False</GenerateTSFile>
        </JsonSchema>
    </ItemGroup>
</Project>
```