using BeetleX.FastHttpApi;

using Forward.Core;
using Forward.ExtensionService;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Reflection;

namespace Forward.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            UnitWork.Instance.Register((services) =>
            {
                services.AddScoped<Forward.Core.Forward>();

                services.AddSingleton<MaillService>();

                services.AddSingleton(typeof(IConfig), (serviceProvider)=>
                {
                    var instance = new Config();
                    instance.TryAdd<MaillService>("maill");
                    return instance;
                });
            });
            UnitWork.Instance.Builder();

            var mApiServer = new HttpApiServer();
            mApiServer.ActionFactory.ControllerInstance += (o, e) =>
            {
                e.Controller = UnitWork.Instance.ServiceProvider.GetRequiredService(e.Type);
            };
            mApiServer.Register(Assembly.Load("Forward.Core"));
            mApiServer.Open();

            Console.ReadLine();
        }
    }
}
