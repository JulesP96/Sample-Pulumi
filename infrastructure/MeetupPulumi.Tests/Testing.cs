using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text.Json;
using System.Threading.Tasks;
using Moq;
using Pulumi;
using Pulumi.Testing;

namespace MeetupPulumi.Tests;

/// <summary>
/// Helper class to unit test a Pulumi stack.
/// </summary>
public static class Testing
{
    /// <summary>
    /// Configuration of the infrastructure project.
    /// </summary>
    private static readonly Dictionary<string, string> ProjectConfiguration = new()
        {
            { "project:prefix", "mtp" },
            { "project:serviceName", "api" },
            { "project:projectName", "WebApi" },
            { "azure-native:location", "westeurope" }
        };

    /// <summary>
    /// Run the stack.
    /// </summary>
    /// <typeparam name="T">The Stack to initialize.</typeparam>
    /// <returns>A list of <see cref="Resource"/>.</returns>
    public static Task<ImmutableArray<Resource>> RunAsync<T>() where T : Stack, new()
    {
        var mocks = new Mock<IMocks>();

        var config = JsonSerializer.Serialize(ProjectConfiguration);
        System.Environment.SetEnvironmentVariable("PULUMI_CONFIG", config);

        mocks.Setup(m => m.NewResourceAsync(It.IsAny<MockResourceArgs>()))
            .ReturnsAsync((MockResourceArgs args) => (args.Id ?? "", args.Inputs));

        mocks.Setup(m => m.CallAsync(It.IsAny<MockCallArgs>()))
            .ReturnsAsync((MockCallArgs args) => args.Args);

        return Deployment.TestAsync<T>(mocks.Object, new TestOptions { IsPreview = false });
    }
}