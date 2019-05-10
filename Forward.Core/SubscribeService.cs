using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;

namespace Forward.Core
{
    public class SubscribeService : IObserver<DiagnosticListener>
    {
        public void AddService<TSourceService, TTargetService>(Func<object, object> func)
            where TSourceService : IForwardService
            where TTargetService : IForwardService
        {
            var builder = UnitWork.Instance.ServiceProvider.GetRequiredService<SubscribeBuilder>();

            builder.Add<TSourceService, TTargetService>(func);
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(DiagnosticListener listener)
        {
            var builder = UnitWork.Instance.ServiceProvider.GetRequiredService<SubscribeBuilder>();

            if (builder.Contains(listener.Name))
            {
                Console.WriteLine($"find");
                
                if (builder.TryGetValue(listener.Name, out var forwardSubscribe))
                {               
                    listener.Subscribe(forwardSubscribe);
                }
            }
        }
    }
}
