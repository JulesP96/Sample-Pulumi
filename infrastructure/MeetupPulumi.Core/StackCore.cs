using System.Collections.Generic;
using MeetupPulumi.Core.Extensions;
using Pulumi;
using Pulumi.AzureNative.Resources;

namespace MeetupPulumi.Core
{
    class StackCore : StackBase
    {
        public ResourceGroup ResourceGroup { get; private set; }
        public Config Configuration { get; private set; }
        public Config AzureConfiguration { get; private set; }
        public Dictionary<string, string> Tags { get; private set; }

        public StackCore()
        {
            Configuration = new Config();
            AzureConfiguration = new Config("azure-native");
            Tags = new Dictionary<string, string>()
            {
                { "Project", Pulumi.Deployment.Instance.ProjectName },
                { "Location", AzureConfiguration.Require("location") },
                { "Environment", Pulumi.Deployment.Instance.StackName }
            };

            // Create an Azure Resource Group
            this.ResourceGroup = new ResourceGroup(typeof(ResourceGroup).BuildName("01"), new ResourceGroupArgs
            {
                ResourceGroupName = typeof(ResourceGroup).BuildName("01"),
                Tags = Tags
            });

            // Create resources

        }
    }
}
