using System;
using System.Collections.Generic;
using Pulumi;
using Pulumi.AzureNative.Maps.V20210201;
using Pulumi.AzureNative.Resources;
using Resource = Pulumi.Resource;

namespace MeetupPulumi.Core.Extensions
{
    public static class ResourceExtensions
    {
        public static string BuildName(this Type resourceType, string id) => NameBuilder(resourceType, id);

        // Update this dictionary to manage the mapping of resources to a name
        private static readonly Dictionary<Type, string> SupportedResources = new Dictionary<Type, string>
        {
            { typeof(ResourceGroup), "rsg" }
        };

        // Update this method to change the naming scheme for resources
        private static string NameBuilder(Type resourceType, string id)
        {
            var configuration = new Config();
            var prefix = configuration.Require("prefix");
            var serviceName = configuration.Require("serviceName");
            var stackName = Pulumi.Deployment.Instance.StackName;
            var resourceName = SupportedResources[resourceType];

            var name = $"{prefix}{stackName}{serviceName}{resourceName}{id}";
            
            return name;
        } 
    }
}