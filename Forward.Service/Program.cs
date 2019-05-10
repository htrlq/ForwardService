using BeetleX.FastHttpApi;

using Forward.Core;
using Forward.ExtensionService;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Diagnostics;
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
                services.AddSingleton<OrderService>();

                services.AddSingleton(typeof(IConfig), (serviceProvider)=>
                {
                    var instance = new Config();
                    instance.TryAdd<MaillService>("maill");
                    return instance;
                });
            });
            UnitWork.Instance.Builder();

            var subscribe = new SubscribeService();

            subscribe.AddService<MaillService, OrderService>((response) =>
            {
                if (response is bool checkResponse)
                {
                    if (checkResponse)
                    {
                        return new OrderModel
                        {
                            Title = "Subscribe and Create Order",
                            PayMoney = 1,
                            CreateTime = DateTime.Now
                        };
                    }
                }

                return null;
            });

            DiagnosticListener.AllListeners.Subscribe(subscribe);

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
