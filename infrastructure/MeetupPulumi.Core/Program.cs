using System.Threading.Tasks;
using Pulumi;

namespace MeetupPulumi.Core
{
    class Program
    {
        static Task<int> Main() => Deployment.RunAsync<StackCore>();
    }
}
