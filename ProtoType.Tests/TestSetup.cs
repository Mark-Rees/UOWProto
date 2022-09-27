using Microsoft.Extensions.DependencyInjection;
using ProtoType.Business.Services;
using System.IO;

namespace ProtoType.Tests
{
    public class TestSetup
    {
        public TestSetup()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<FixtureService, FixtureService>();
            serviceCollection.AddTransient<ResultService, ResultService>();
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public ServiceProvider ServiceProvider { get; private set; }
    }
}
