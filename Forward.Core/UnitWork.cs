using Microsoft.Extensions.DependencyInjection;
using System;

namespace Forward.Core
{
    public class UnitWork
    {
        public static UnitWork Instance = new UnitWork();
        private IServiceCollection ServiceCollection { get; } = new ServiceCollection();
        public IServiceProvider ServiceProvider { get; set; }

        public IServiceCollection Register(Action<IServiceCollection> func)
        {
            func(ServiceCollection);

            ServiceCollection.AddSingleton<IForwardFactory, ForwardFactory>();
            ServiceCollection.AddSingleton<SubscribeBuilder>();

            return ServiceCollection;
        }

        public void Builder()
        {
            ServiceProvider = ServiceCollection.BuildServiceProvider();
        }
    }
}
