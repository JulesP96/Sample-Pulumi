using System.Linq;
using System.Threading.Tasks;
using MeetupPulumi.Core;
using Xunit;
using Pulumi.AzureNative.Web;
using Pulumi.Utilities;

namespace MeetupPulumi.Tests.Modules;

public class AppPlanModuleTests
{
    [Fact]
    public async Task Constructor_ShouldInitializeAppServicePlan()
    {
        // arrange
        var resources = await Testing.RunAsync<StackCore>();
        var expectedName = "mtpstackapiasp01";
        
        // act
        var appServicePlan = resources.OfType<AppServicePlan>().First();
        var tags = await OutputUtilities.GetValueAsync(appServicePlan.Tags);
        var sku = await OutputUtilities.GetValueAsync(appServicePlan.Sku);

        // assert
        Assert.NotNull(appServicePlan);
        Assert.NotNull(tags);
        Assert.NotNull(sku);
        Assert.Equal(1, sku!.Capacity);
        Assert.Equal("B", sku!.Family);
        Assert.Equal("B1", sku!.Name);
        Assert.Equal("B1", sku!.Size);
        Assert.Equal("Basic", sku!.Tier);
        Assert.Equal("project", tags!["Project"]);
        Assert.Equal("stack", tags!["Environment"]);
        Assert.Equal(expectedName, appServicePlan.GetResourceName());
    }
    
    [Fact]
    public async Task Constructor_ShouldInitializeAppService()
    {
        // arrange
        var resources = await Testing.RunAsync<StackCore>();
        var expectedName = "mtpstackapiapp01";
        
        // act
        var appServicePlan = resources.OfType<AppServicePlan>().First();
        var appServicePlanId = await OutputUtilities.GetValueAsync(appServicePlan.Id);
        var webApp = resources.OfType<WebApp>().First();
        var tags = await OutputUtilities.GetValueAsync(webApp.Tags);
        var serverFarmId = await OutputUtilities.GetValueAsync(webApp.ServerFarmId);

        // assert
        Assert.NotNull(webApp);
        Assert.NotNull(tags);
        Assert.Equal(appServicePlanId, serverFarmId);
        Assert.Equal("project", tags!["Project"]);
        Assert.Equal("stack", tags!["Environment"]);
        Assert.Equal(expectedName, webApp.GetResourceName());
    }

}