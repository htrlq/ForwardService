using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Forward.Core
{
    internal class SubscribeBuilder
    {
        private List<string> list = new List<string>();

        private ConcurrentDictionary<string, ForwardServiceSubscribe> dictionary = new ConcurrentDictionary<string, ForwardServiceSubscribe>();

        public void Add<TSourceService,TTargetService>(Func<object, object> func)
            where TSourceService : IForwardService
            where TTargetService: IForwardService
        {
            var key = typeof(TSourceService).FullName;

            if (list.Contains(key))
                throw new Exception($"Contains Key:{key}");

            list.Add(key);

            dictionary.TryAdd(key, new ForwardServiceSubscribe(UnitWork.Instance.ServiceProvider.GetRequiredService<TTargetService>(), func));
        }

        public bool Contains(string value)
        {
            return list.Contains(value);
        }

        public bool TryGetValue(string key, out ForwardServiceSubscribe value)
        {
            return dictionary.TryGetValue(key, out value);
        }
    }
}
