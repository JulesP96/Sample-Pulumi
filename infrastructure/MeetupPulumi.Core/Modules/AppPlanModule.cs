using MeetupPulumi.Core.Extensions;
using Pulumi;
using Pulumi.AzureNative.Web;
using Pulumi.AzureNative.Web.Inputs;

namespace MeetupPulumi.Core.Modules
{
    public class AppPlanModule
    {
        public Output<string> ApiName { get; }

        public AppPlanModule(StackCore stack)
        {
            var appServicePlan = new AppServicePlan(typeof(AppServicePlan).BuildName("01"), new AppServicePlanArgs()
            {
                Name = typeof(AppServicePlan).BuildName("01"),
                Kind = "app",
                ResourceGroupName = stack.ResourceGroup.Name,
                Sku = new SkuDescriptionArgs()
                {
                    Capacity = 1,
                    Family = "B",
                    Name = "B1",
                    Size = "B1",
                    Tier = "Basic"
                },

                Tags = stack.Tags
            });

            var api = new WebApp(typeof(WebApp).BuildName("01"), new WebAppArgs()
            {
                Name = typeof(WebApp).BuildName("01"),
                ResourceGroupName = stack.ResourceGroup.Name,
                ServerFarmId = appServicePlan.Id,

                Tags = stack.Tags
            });

            this.ApiName = api.Name;
        } 
    }
}